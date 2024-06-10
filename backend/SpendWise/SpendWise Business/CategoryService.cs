using Microsoft.EntityFrameworkCore;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IRepository<Product> _productsRepository;

        public CategoryService(ICategoryRepository categoriesRepository, IRepository<Product> productsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _productsRepository = productsRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesDtoAsync()
        {
            var categories = await _categoriesRepository.GetAllAsync();

            var categoriesDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
            }).ToList();

            return categoriesDtos;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _categoriesRepository.GetAllAsync();

            return categories;
        }

        public async Task<IEnumerable<CategoryDisplayDto>> CategoriesAsync()
        {
            var categories = await _categoriesRepository.GetAllAsync();

            var categoriesDtos = categories.Select(c => new CategoryDisplayDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            return categoriesDtos;
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var category = await _categoriesRepository.FindByIdAsync(id);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(p => new ProductCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
            };
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };
            return await _categoriesRepository.PostAsync(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Category category)
        {
            var categoryCreated = await _categoriesRepository.UpdateAsync(category);

            return new CategoryDto
            {
                Id = categoryCreated.Id,
                Name = categoryCreated.Name,
                Products = categoryCreated.Products.Select(p => new ProductCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
            };
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoriesRepository.DeleteAsync(id);
        }

        public async Task<List<CategoryDataDto>> GetCategoriesTotalPrice(DateTime dateFrom, DateTime dateTo)
        {
            var categories = await GetCategoriesAsync();

            var categoriesData = new List<CategoryDataDto>();

            foreach (Category category in categories)
            {
                categoriesData.Add(new CategoryDataDto
                {
                    Name = category.Name,
                    TotalPrice = category.Products.Sum(p => p.CartProducts.Where(cp => cp.Cart.Date >= dateFrom && cp.Cart.Date <= dateTo).Sum(cp => cp.Price * cp.Quantity))
                });
            }

            return categoriesData;
        }

        public async Task<List<CategorySpendingDto>> GetCategoriesSpendingAsync(DateTime? dateFrom, DateTime? dateTo)
        {
            var categories = await _categoriesRepository.GetAllAsync();
            var categoriesSpending = new List<CategorySpendingDto>();

            foreach (var category in categories)
            {
                double sum = 0;

                foreach (var product in category.Products)
                {
                    if (dateFrom != null && dateTo != null)
                    {
                        sum += product.CartProducts.Where(c => c.Cart.Date >= dateFrom && c.Cart.Date <= dateTo)
                        .Sum(c => c.Price * c.Quantity);
                    }
                    else
                    {
                        sum += product.CartProducts.Sum(c => c.Price * c.Quantity);
                    }
                }

                var categorySpending = new CategorySpendingDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    TotalSpent = sum
                };

                categoriesSpending.Add(categorySpending);
            }

            return categoriesSpending;
        }

        public async Task<ProductDto> AddCategoryToProductAsync(int productId, int categoryId)
        {
            var category = await _categoriesRepository.FindByIdAsync(categoryId);
            var product = await _productsRepository.FindByIdAsync(productId);

            if (category == null || product == null) return null;

            var newProduct = await _categoriesRepository.AddCategoryToProductAsync(product, category);

            return new ProductDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Categories = newProduct.Categories.Select(c => c.Name).ToArray()
            };
        }

        public async Task<CategoryDto> RemoveCategoryFromProductAsync(int productId, int categoryId)
        {
            var category = await _categoriesRepository.FindByIdAsync(categoryId);
            var product = await _productsRepository.FindByIdAsync(productId);

            if (category == null || product == null) return null;

            await _categoriesRepository.RemoveCategoryFromProductAsync(product, category);
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(product => new ProductCategoryDto
                {
                    Id = product.Id,
                    Name = product.Name
                })
            };
        }
    }
}
