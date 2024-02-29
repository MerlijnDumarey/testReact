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
    public class LectorsController : ControllerBase
    {
        private readonly ILectorsService _lectorsService;

        public LectorsController(ILectorsService lectorsService)
        {
            _lectorsService = lectorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lectors = await _lectorsService.GetAsync();
            var lectorsGetAllDto = new LectorsGetAllDto();
            lectorsGetAllDto.Items = lectors.Select(l =>
                new LectorsGetByIdDto()
                {
                    Id = l.Id,
                    Name = l.Name,
                    Email = l.Email,
                    DateOfBirth = l.Dob,
                    FamilyName = l.FamilyName
                });
            return Ok(lectorsGetAllDto);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var lector = await _lectorsService.GetAsync(id);
            if (lector == null)
            {
                return NotFound();
            }
            var lectorsGetByIdDto = new LectorsGetByIdDto
            {
                Id = lector.Id,
                Name = lector.Name,
                Email = lector.Email,
                DateOfBirth = lector.Dob,
                FamilyName = lector.FamilyName
            };
            return Ok(lectorsGetByIdDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LectorsCreateDto lectorsCreateDto)
        {
            await _lectorsService.CreateAsync(new Lector()
            {
                Name = lectorsCreateDto.Name,
                Email = lectorsCreateDto.Email,
                Dob = lectorsCreateDto.DateOfBirth,
                FamilyName = lectorsCreateDto.FamilyName
            });
            return Ok("Created");
        }

        [HttpPut]
        public async Task<IActionResult> Update(LectorsUpdateDto lectorsUpdateDto)
        {
            await _lectorsService.UpdateAsync(lectorsUpdateDto.Id, new Lector()
            {
                Id = lectorsUpdateDto.Id,
                Name = lectorsUpdateDto.Name,
                Email = lectorsUpdateDto.Email,
                Dob = lectorsUpdateDto.DateOfBirth,
                FamilyName = lectorsUpdateDto.FamilyName
            });
            return Ok("Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _lectorsService.RemoveAsync(id);
            return Ok("Deleted");
        }
    }
}
