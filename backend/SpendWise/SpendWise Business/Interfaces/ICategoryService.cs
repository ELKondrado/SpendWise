using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_Business.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesDtoAsync();
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<CategoryDisplayDto>> CategoriesAsync();
        Task<CategoryDto> GetCategoryAsync(int id);
        Task<Category> CreateCategoryAsync(CreateCategoryDto category);
        Task<CategoryDto> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<List<CategorySpendingDto>> GetCategoriesSpendingAsync(DateTime? dateFrom, DateTime? dateTo);
        Task<ProductDto> AddCategoryToProductAsync(int productId, int categoryId);
        Task<CategoryDto> RemoveCategoryFromProductAsync(int productId, int categoryId);

    }
}
