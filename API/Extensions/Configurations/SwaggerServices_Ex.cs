using Microsoft.OpenApi.Models;
using System;
using System.Xml.Linq;

namespace API.Extensions.Configurations
{
    public static class SwaggerServices_Ex
    {

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiNet Api", Version = "v1" });

                var SecuritySchema = new OpenApiSecurityScheme
                {


                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",

                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", SecuritySchema);

                var secuirtyReq = new OpenApiSecurityRequirement { { SecuritySchema, new[] { "Bearer" } } };

                c.AddSecurityRequirement(secuirtyReq);
            });
                return services;
            }


        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                return app;

            }
        }
    }
