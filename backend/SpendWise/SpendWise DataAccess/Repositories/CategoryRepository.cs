using Microsoft.EntityFrameworkCore;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using SpendWise_DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly SpendWiseContext _context;

        public CategoryRepository(SpendWiseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                return await _context.Categories.Include(c => c.Products).ThenInclude(p => p.CartProducts).ThenInclude(cp => cp.Cart).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving categories from DB: {ex.Message}", ex);
            }
        }

        public async Task<Category?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving the category by id: {ex.Message}", ex);
            }
        }

        public async Task<Category> PostAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when adding category to DB: {ex.Message}", ex);
            }
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                _context.Categories.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when updating the category from DB: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                
                if (category == null)
                {
                    return;
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when deleting the category from DB: {ex.Message}", ex);
            }
        }

        public async Task<Product> AddCategoryToProductAsync(Product product, Category category)
        {
            if (!product.Categories.Contains(category))
            {
                product.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
            
            return product;
        }

        public async Task<Category> RemoveCategoryFromProductAsync(Product product, Category category)
        {
            if (!product.Categories.Contains(category))
            {
                return null;
            }
            product.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
