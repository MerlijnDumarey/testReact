using BeFit.MongoDb.Api.DTOs;
using BeFit.MongoDb.Api.DTOs.Request;
using BeFit.MongoDb.Api.DTOs.Response;
using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.MongoDb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestsService _testsService;

        public TestsController(ITestsService testsService)
        {
            _testsService = testsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tests = await _testsService.GetAsync();
            var testsGetAllDto = new TestsGetAllDto();
            testsGetAllDto.Items = tests.Select(c => 
                new TestsGetByIdDto 
                {
                    Id = c.Id, 
                    Name = c.Name,
                    Category = new BaseDto { Id = c.Category.Id, Name = c.Category.Name},
                    Description = c.Description,
                    Unit = c.Unit,
                    ComparisonType = c.ComparisonType,
                    HigherBound = c.HigherBound,
                    LowerBound = c.LowerBound,
                });
            return Ok(testsGetAllDto);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var test = await _testsService.GetAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            var testsGetByIdDto = new TestsGetByIdDto
            {
                Id = test.Id,
                Name = test.Name,
                Category = new BaseDto { Id = test.Category.Id, Name = test.Category.Name },
                Description = test.Description,
                Unit = test.Unit,
                ComparisonType = test.ComparisonType,
                HigherBound = test.HigherBound,
                LowerBound = test.LowerBound,
            };
            return Ok(testsGetByIdDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestsCreateDto testsCreateDto)
        {
            await _testsService.CreateAsync(testsCreateDto.Name, testsCreateDto.CategoryId, testsCreateDto.Description, testsCreateDto.Unit, testsCreateDto.ComparisonType, testsCreateDto.LowerBound, testsCreateDto.HigherBound);
            return Ok("Created");
        }

        [HttpPut]
        public async Task<IActionResult> Update(TestsUpdateDto testsUpdateDto)
        {
            await _testsService.UpdateAsync(testsUpdateDto.Id, testsUpdateDto.Name, testsUpdateDto.CategoryId, testsUpdateDto.Description, testsUpdateDto.Unit, testsUpdateDto.ComparisonType, testsUpdateDto.LowerBound, testsUpdateDto.HigherBound);
            return Ok("Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _testsService.RemoveAsync(id);
            return Ok("Deleted");
        }
    }
}
