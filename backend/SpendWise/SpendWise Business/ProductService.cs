using SpendWise_Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using SpendWise_DataAccess.Repositories;
using SpendWise_DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_Business
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IRepository<Product> productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Categories = p.Categories.Select(c => c.Name)
            }).ToList();
            return productsDto;
        }
       
        public async Task<ProductDisplayDto?> GetProductAsync(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            
            if (product == null)
            {
                return null;
            }

            return new ProductDisplayDto
            {
                Id = product.Id,
                Name = product.Name,
                Categories = product.Categories.Select(c => c.Name)
            };
        }

        public async Task<ProductDisplayDto> CreateProductAsync(CreateProductDto productDto)
        {
            var categories = await _categoryRepository.GetAllAsync();

            var product = new Product
            {
                Name = productDto.Name,
                Categories = categories.Where(c => productDto.CategoryIds.Contains(c.Id)).ToList()
            };

            var addedProduct = await _productRepository.PostAsync(product);

            return new ProductDisplayDto
            {
                Id = addedProduct.Id,
                Name = addedProduct.Name,
                Categories = addedProduct.Categories.Select(c => c.Name)
            };
        }
    }
}
