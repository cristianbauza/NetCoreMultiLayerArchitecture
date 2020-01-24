using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace WebAPI.Infreaestructure
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Net Core API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

                //services.AddAuthentication(option =>
                //{
                //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                //});
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration conf)
        {
            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "help";
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Net Core API");
            });

            return app;
        }
    }
}