using BeFit.MongoDb.Api.DTOs;
using BeFit.MongoDb.Api.DTOs.Response;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.MongoDb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet("GetAllAttemptsByStudent")]
        public async Task<IActionResult> GetAttemptsByStudent(string studentId)
        {
            var attempts = await _searchService.GetAttemptsByStudent(studentId);
            var attemptsGetAllDto = new SearchAttemptsByStudentIdDto();
            attemptsGetAllDto.Items = attempts.Select(a => new AttemptsGetByIdDto
            {
                Id = a.Id,
                Date = a.Date,
                Lector = new BasePersonDto() { Id = a.Lector.Id, Name = a.Lector.Name, FamilyName = a.Lector.FamilyName },
                Score = a.Score,
                TestDescription = a.Test.Description,
                Test = new BaseDto() { Id = a.Test.Id, Name = a.Test.Name }

            });
            return Ok(attemptsGetAllDto);
        }
        [HttpGet("GetAllAttemptsByLector")]
        public async Task<IActionResult> GetAttemptsByLector(string lectorId)
        {
            var attempts = await _searchService.GetAttemptsByLector(lectorId);
            var attemptsGetAllDto = new SearchAttemptsByLectorIdDto();
            attemptsGetAllDto.Items = attempts.Select(a => new AttemptsGetByIdDto
            {
                Id = a.Id,
                Date = a.Date,
                Student = new BasePersonDto() { Id = a.Student.Id, Name = a.Student.Name, FamilyName = a.Student.FamilyName },
                Score = a.Score,
                TestDescription = a.Test.Description,
                Test = new BaseDto() { Id = a.Test.Id, Name = a.Test.Name }
            });
            return Ok(attemptsGetAllDto);
        }
        [HttpGet("GetAllAttemptsByTest")]
        public async Task<IActionResult> GetAttemptsByTest(string testId)
        {
            var attempts = await _searchService.GetAttemptsByTest(testId);
            var attemptsGetAllDto = new SearchAttemptsByTestIdDto();
            attemptsGetAllDto.Attempts = attempts.Select(a => new AttemptsGetByIdDto
            {
                Id = a.Id,
                Date = a.Date,
                Lector = new BasePersonDto() { Id = a.Lector.Id, Name = a.Lector.Name, FamilyName = a.Lector.FamilyName },
                Score = a.Score,
                Student = new BasePersonDto() { Id = a.Student.Id, Name = a.Student.Name, FamilyName = a.Student.FamilyName }
            });
            return Ok(attemptsGetAllDto);
        }
        [HttpGet("GetTestsByCategory")]
        public async Task<IActionResult> GetTestsByCategory(string categoryId)
        {
            var tests = await _searchService.GetTestsByCategory(categoryId);
            var testsGetAllDto = new SearchTestsByCategoryIdDto();
            testsGetAllDto.Tests = tests.Select(t => new TestsGetByIdDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Category = new BaseDto() { Id = t.Category.Id, Name = t.Category.Name }
            });
            return Ok(testsGetAllDto);
        }
    }
}
