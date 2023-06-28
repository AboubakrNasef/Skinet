using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepositry
    {
        Task<Product> GetProductByIdAsunc(int id);

        Task<IReadOnlyList<Product>> GetProductsAsync();

        Task<IReadOnlyList<Product>> GetProductTypesAsync();

        Task<IReadOnlyList<Product>> GetProductBrandsAsync();

    }
}
