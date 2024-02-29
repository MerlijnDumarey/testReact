using BeFit.MongoDb.Api.DTOs.Request;
using BeFit.MongoDb.Api.DTOs.Response;
using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.MongoDb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoriesService.GetAsync();
            var categoriesGetAllDto = new CategoriesGetAllDto();
            categoriesGetAllDto.Items = categories.Select(c => new CategoriesGetByIdDto { Id = c.Id, Name = c.Name });
            return Ok(categoriesGetAllDto);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _categoriesService.GetAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            var categoriesGetByIdDto = new CategoriesGetByIdDto()
            {
                Id = category.Id,
                Name = category.Name,
            };
            return Ok(categoriesGetByIdDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriesCreateDto categoryCreateDto)
        {
            await _categoriesService.CreateAsync(new Category() { Name = categoryCreateDto.Name });
            return Ok("Created");
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoriesUpdateDto categoryUpdateDto)
        {
            await _categoriesService.UpdateAsync(categoryUpdateDto.Id, new Category() { Id = categoryUpdateDto.Id, Name = categoryUpdateDto.Name });
            return Ok("Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _categoriesService.RemoveAsync(id);
            return Ok("Deleted");
        }
    }
}
