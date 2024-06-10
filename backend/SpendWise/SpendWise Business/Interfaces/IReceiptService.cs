using Microsoft.AspNetCore.Http;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_Business.Interfaces
{
    public interface IReceiptService
    {
        public Task<List<CategorizedProductsDto>> ScanReceipt(List<Category> categories, IFormFile image);
        public Task<Cart> SaveCart(CartCreateDto cartDto);
    }
}
