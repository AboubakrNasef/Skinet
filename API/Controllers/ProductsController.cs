
using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
  
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepositry<Product> _productRepo;
        private readonly IGenericRepositry<ProductBrand> _brandRepo;
        private readonly IGenericRepositry<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepositry<Product> productRepo,
            IGenericRepositry<ProductBrand> brandRepo,
            IGenericRepositry<ProductType> typeRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParam specParam)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(specParam);
            var countSpec = new ProductWithFilterForCountSpecification(specParam);
            var totalItems = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(specParam.PageIndex,specParam.PageSize,totalItems,data));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }


            return Ok(
                _mapper.Map<Product, ProductDto>(product)); 
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var products = await _typeRepo.ListAllAsync();

            return Ok(products);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var products = await _brandRepo.ListAllAsync();

            return Ok(products);
        }




    }
}
