using Application.Common;
using FluentValidation;
using MediatR;
using NSwag.Generation.Processors.Security;
using NSwag;
using System.Reflection;
using WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUi(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "InvoiceMgt API";
                configure.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
            });
            return services;
        }
    }
}
