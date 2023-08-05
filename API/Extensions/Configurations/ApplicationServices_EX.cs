using API.Errors;
using Core.Interfaces;
using Infrastructure.Repositries;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
namespace API.Extensions.Configurations
{
    public static class ApplicationServices_EX
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            services.AddScoped<IProductRepositry, ProductRepositry>();
            services.AddScoped<IBasketRepository, BasketRepositry>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.Configure<ApiBehaviorOptions>(options =>
                                        options.InvalidModelStateResponseFactory = actionContext =>
                                          {

                                              var errors = actionContext.ModelState
                                              .Where(e => e.Value.Errors.Count > 0)
                                              .SelectMany(x => x.Value.Errors)
                                              .Select(x => x.ErrorMessage).ToArray();

                                              var errorResponse = new ApiValidationErrorResponse { Errors = errors };


                                              return new BadRequestObjectResult(errorResponse);
                                          });

            return services;
        }

    }
}
