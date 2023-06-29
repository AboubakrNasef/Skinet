using System;
namespace API.Extensions.Configurations
{
    public static class SwaggerServices_Ex
    {

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {

          services.AddSwaggerGen();

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
