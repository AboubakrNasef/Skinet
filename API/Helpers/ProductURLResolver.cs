using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductURLResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;

        public ProductURLResolver(IConfiguration config)
        {
            this._config = config;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}
