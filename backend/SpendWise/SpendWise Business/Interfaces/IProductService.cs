using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDisplayDto?> GetProductAsync(int id);
        Task<ProductDisplayDto> CreateProductAsync(CreateProductDto product);
    }
}
