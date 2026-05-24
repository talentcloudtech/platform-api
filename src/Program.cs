using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.API.Common;
using Platform.API.Configurations;
using Platform.Application.Common.Services;
using System.Threading.RateLimiting;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.AddValidationSetup();

// Identity & Auth
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddAuthorization();

// Swagger (registered always; exposed only in non-production)
builder.Services.AddSwaggerSetup();

// Persistence
builder.Services.AddPersistenceSetup(builder.Configuration);

// Application layer setup
builder.Services.AddApplicationSetup();

builder.Services.AddCompressionSetup();

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Mediator
builder.Services.AddMediatRSetup();

// Exception handler
builder.Services.AddExceptionHandler<ExceptionHandler>();

// Blob storage
builder.Services.AddScoped<IBlobStorageService, AzureBlobStorageService>();

// IOptionConfigure
builder.Services.ConfigureAllOptions(builder.Configuration);
builder.Logging.ClearProviders();

// Add serilog
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingSetup(builder.Configuration);

    // Add open telemetry
    builder.AddOpenTelemetrySetup();
}

// CORS — restrict to known frontend origin only
var frontendUrl = EnvironmentVariables.FrontendUrl;
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(frontendUrl)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

// Rate limiting — protect auth endpoints from brute-force & flooding
builder.Services.AddRateLimiter(options =>
{
    // Strict limiter for sensitive auth endpoints (login, register, forgot-password, refresh)
    options.AddFixedWindowLimiter("auth", opt =>
    {
        opt.PermitLimit = 10;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0;
    });

    // General API limiter applied globally
    options.AddFixedWindowLimiter("global", opt =>
    {
        opt.PermitLimit = 200;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 5;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "database");

var app = builder.Build();

// Order is critical here!
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Ensure CORS is applied before routing, authentication, and authorization
app.UseCors();

// Apply routing middleware after CORS
app.UseRouting();

app.UseHsts();
app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapAllEndpoints();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Swagger — only exposed outside production to avoid leaking API schema
if (!app.Environment.IsProduction())
{
    app.UseSwaggerSetup();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}
else
{
    app.MapGet("/", () => Results.Ok(new { status = "Platform API is running" }));
}

// Health check endpoint
app.MapHealthChecks("/health");

await app.RunAsync();
