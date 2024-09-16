using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.Api.Common;
using Platform.Api.Configurations;
using Platform.API.Configurations;
using Platform.Api.Endpoints;
using Platform.API.Endpoints;

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseSwaggerSetup();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

app.UseHsts();
app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapProjectEndpoints();
app.MapMemberProfileEndpoints();
app.MapFileStorageEndpoints();
app.MapWebhookEndpoints();
app.MapAccountEndpoints();

app.MapGet("/", () => Results.Redirect("/swagger"));

await app.RunAsync();
