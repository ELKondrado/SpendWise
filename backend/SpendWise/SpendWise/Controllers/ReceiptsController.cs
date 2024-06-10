using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SpendWise_Business.Exceptions;
using SpendWise_Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;

namespace SpendWise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService _receiptService;

        public ReceiptsController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost("ScanReceipt")]
        public async Task<ActionResult<string>> ScanReceipt([FromForm] string categories, [FromForm] IFormFile image)
        {
            List<Category> categoriesList = JsonConvert.DeserializeObject<List<Category>>(categories) ?? new List<Category>();

            if (categoriesList.IsNullOrEmpty() || image.Length <= 0)
            {
                return BadRequest();
            }

            var categorizedProductsDto = await _receiptService.ScanReceipt(categoriesList, image);

            return Ok(categorizedProductsDto);
        }

        [HttpPost("SaveCart")]
        public async Task<ActionResult> SaveCart([FromBody]CartCreateDto cart)
        {
            try
            {
                if (cart.CategoryProducts.IsNullOrEmpty())
                {
                    return BadRequest();
                }

                var createdCart = await _receiptService.SaveCart(cart);

                return Ok();
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
