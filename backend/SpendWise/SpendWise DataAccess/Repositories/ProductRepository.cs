using Microsoft.EntityFrameworkCore;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(SpendWiseContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _context.Products.Include(p => p.Categories).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving products from DB: {ex.Message}", ex);
            }
        }

        public override async Task<Product?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving the product by id: {ex.Message}", ex);
            }
        }

        public async Task<Product?> FindByNameAsync(string name)
        {
            try
            {
                return await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Name == name);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving the product by id: {ex.Message}", ex);
            }
        }

        public override async Task<Product> PostAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when adding product to DB: {ex.Message}", ex);
            }
        }
    }
}
