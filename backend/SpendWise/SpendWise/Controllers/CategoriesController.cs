using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpendWise_Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;
using System.Collections.Generic;

namespace SpendWise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoriesService;

        public CategoriesController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoriesService.GetCategoriesDtoAsync();
            return Ok(categories);
        }

        [HttpGet()]
        public async Task<IActionResult> Categories()
        {
            var categories = await _categoriesService.CategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("GetCategory/{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoriesService.GetCategoryAsync(id);
            return Ok(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> AddCategory([FromBody]CreateCategoryDto category)
        {
            var categoryCreated = await _categoriesService.CreateCategoryAsync(category);
            return Ok(categoryCreated);
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody]Category category)
        {
            if(category.Id != id)
            {
                return BadRequest("Category ID mismatch");
            }
            var categoryUpdated = await _categoriesService.UpdateCategoryAsync(category);
            return Ok(categoryUpdated);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoriesService.DeleteCategoryAsync(id);
            return Ok();
        }

        [HttpGet("GetCategoriesSpending")]
        public async Task<IActionResult> GetCategoriesSpending([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            var categoriesSpending = await _categoriesService.GetCategoriesSpendingAsync(dateFrom, dateTo);

            return Ok(JsonConvert.SerializeObject(categoriesSpending));
        }

        [HttpPost("AddCategoryToProduct/{productId}/{categoryId}")]
        public async Task<IActionResult> AddCategoryToProduct(int productId, int categoryId)
        {
            var newProduct = await _categoriesService.AddCategoryToProductAsync(productId, categoryId);

            return Ok(newProduct);
        }

        [HttpDelete("RemoveCategoryToProduct/{productId}/{categoryId}")]
        public async Task<IActionResult> RemoveCategoryToProduct(int productId, int categoryId)
        {
            var removedCategory = await _categoriesService.RemoveCategoryFromProductAsync(productId, categoryId);

            return Ok(removedCategory);
        }
    }
}
