
using API.DTOs;
using API.Errors;
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
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList< ProductDTO >>(products));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDTO), 200)]
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
                _mapper.Map<Product, ProductDTO>(product)); 
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
