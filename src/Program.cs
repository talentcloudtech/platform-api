using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
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

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwaggerSetup();
}
app.UseSwaggerSetup();

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

app.UseHsts();
app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapFileStorageEndpoints();
app.MapWebhookEndpoints();
app.MapAccountEndpoints();
app.MapMemberProfileEndpoints();
app.MapSkillsEndpoints();
app.MapWorkspaceEndpoints();
app.MapWorkspaceMemberEndpoints();
app.MapWorkspaceMemberInvitationEndpoints();
app.MapProjectEndpoints();
app.MapProjectMemberEndpoints();
app.MapProjectTaskEndpoints();
app.MapContractEndpoints();
app.MapMemberRequestEndpoints();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

await app.RunAsync();
