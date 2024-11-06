using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Platform.API.Common;
using Platform.API.Configurations;
using Platform.Application.Common.Services;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.AddValidationSetup();

// identity
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddAuthorization();

// Swagger
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

//IOptionConfigure
builder.Services.ConfigureAllOptions(builder.Configuration);
builder.Logging.ClearProviders();

// Add serilog
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingSetup(builder.Configuration);

    // Add open telemetry
    builder.AddOpenTelemetrySetup();
}

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Order is critical here!
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

// Ensure CORS is applied before routing, authentication, and authorization
app.UseCors();

// Apply routing middleware after CORS
app.UseRouting();

app.UseHsts();
app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Replace all individual endpoint mappings with this single line
app.MapAllEndpoints();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseSwaggerSetup();
app.MapGet("/", () => Results.Redirect("/swagger"));

await app.RunAsync();
