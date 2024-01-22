using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParam specParam) :
            base(x =>
             (String.IsNullOrEmpty(specParam.Search) || x.Name.ToLower().Contains(specParam.Search)) &&
            (!specParam.BrandId.HasValue || x.ProductBrandId == specParam.BrandId)
            &&
            (!specParam.TypeId.HasValue || x.ProductTypeId == specParam.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(specParam.PageSize*(specParam.PageIndex-1), specParam.PageSize);

            if (!string.IsNullOrEmpty(specParam.Sort))
            {
                switch (specParam.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
