using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Platform.Domain.Entities.Common;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Platform.Api.Configurations;

/// <summary>
/// Provides extension methods to configure Swagger for API documentation.
/// </summary>
public static class SwaggerSetup
{
    /// <summary>
    /// Adds and configures Swagger services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <returns>The IServiceCollection with Swagger services added.</returns>
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Platform.Api",
                    Version = "v1",
                    Description = "API Platform",
                    Contact = new OpenApiContact
                    {
                        Name = "Talent Cloud",
                        Url = new Uri("https://www.talentcloudtech.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Copyright to Talent Cloud",
                        Url = new Uri("https://www.talentcloudtech.com")
                    }
                }
            );
            c.DescribeAllParametersInCamelCase();
            c.OrderActionsBy(x => x.RelativePath);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            c.OperationFilter<SecurityRequirementsOperationFilter>();

            // To Enable authorization using Swagger (JWT)
            c.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Enter your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVcJ9\"",
                }
            );

            // Maps all structured ids to the guid type to show correctly on swagger
            var allGuids = typeof(IGuid)
                .Assembly.GetTypes()
                .Where(type => typeof(IGuid).IsAssignableFrom(type) && !type.IsInterface)
                .ToList();

            foreach (var guid in allGuids)
            {
                c.MapType(guid, () => new OpenApiSchema { Type = "string", Format = "uuid" });
            }
        });
        return services;
    }

    /// <summary>
    /// Configures the application to use Swagger and Swagger UI.
    /// </summary>
    /// <param name="app">The IApplicationBuilder to configure.</param>
    /// <returns>The IApplicationBuilder with Swagger and Swagger UI configured.</returns>
    public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DocExpansion(DocExpansion.List);
                c.DisplayRequestDuration();
            });
        return app;
    }
}
