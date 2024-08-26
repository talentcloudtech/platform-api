using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.Api.Common;
using Platform.Api.Configurations;
using Platform.API.Configurations;
using Platform.Api.Endpoints;
using Platform.API.Endpoints;
using Platform.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.AddValidationSetup();

// add logto authorization
builder.Services.AddLogtoAuthorizationSetup(builder.Configuration);

builder.Services.AddAuthorization();

// Swagger
builder.Services.AddSwaggerSetup();

// Persistence
builder.Services.AddPersistenceSetup(builder.Configuration);

// Application layer setup
builder.Services.AddApplicationSetup();

// Add identity stuff
builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();

// Request response compression
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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseSwaggerSetup();
app.UseHsts();

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapProjectEndpoints();
app.MapMemberProfileEndpoints();
app.MapFileStorageEndpoints();

// app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<ApplicationUser>();
app.MapGet("/", () => Results.Redirect("/swagger"));

await app.RunAsync();
