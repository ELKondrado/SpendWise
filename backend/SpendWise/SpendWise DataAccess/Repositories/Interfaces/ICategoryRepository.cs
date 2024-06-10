using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> FindByIdAsync(int id);
        Task<Category> PostAsync(Category entity);
        Task<Category> UpdateAsync(Category entity);
        Task DeleteAsync(int id);
        Task<Product> AddCategoryToProductAsync(Product product, Category category);
        Task<Category> RemoveCategoryFromProductAsync(Product product, Category category);
    }
}
