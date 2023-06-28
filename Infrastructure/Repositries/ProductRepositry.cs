using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositries
{
    public class ProductRepositry : IProductRepositry
    {
        private  readonly StoreContext _storeContext;

        public ProductRepositry(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

    
        public async Task<Product> GetProductByIdAsunc(int id)
        {
            return await _storeContext.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
          return await  _storeContext.Products.ToListAsync();
        }
    }
}
