using BeFit.Core;
using BeFit.MongoDb.Api.DTOs;
using BeFit.MongoDb.Api.DTOs.Request;
using BeFit.MongoDb.Api.DTOs.Response;
using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.MongoDb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : Controller
    {
        private readonly IAttemptsService _attemptService;


        public AttemptController(IAttemptsService attemptsService)
        {
            _attemptService = attemptsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attempts = await _attemptService.GetAsync();
            var attemptsGetAllDto = new AttemptsGetAllDto();
            attemptsGetAllDto.Items = attempts.Select(a => new AttemptsGetByIdDto 
            { 
                Id = a.Id, 
                Date = a.Date, 
                Lector = new BasePersonDto{ Id = a.Lector.Id, Name = a.Lector.Name, FamilyName = a.Lector.FamilyName},
                Score = a.Score,
                Student = new BasePersonDto() { Id = a.Student.Id, Name = a.Student.Name, FamilyName = a.Student.FamilyName },
                TestDescription = a.Test.Description,
                Test = new BaseDto() { Id = a.Test.Id, Name = a.Test.Name }

            });
            return Ok(attemptsGetAllDto);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var attempt = await _attemptService.GetAsync(id);
            if(attempt == null)
            {
                return NotFound();
            }
            var attemptsGetByIdDto = new AttemptsGetByIdDto()
            {
                Id = attempt.Id,
                Date = attempt.Date,
                Lector = new BasePersonDto() { Id = attempt.Lector.Id, Name = attempt.Lector.Name, FamilyName = attempt.Lector.FamilyName },
                Score = attempt.Score,
                Student = new BasePersonDto() { Id = attempt.Student.Id, Name = attempt.Student.Name, FamilyName = attempt.Student.FamilyName },
                TestDescription = attempt.Test.Description,
                Test = new BaseDto() { Id = attempt.Test.Id, Name = attempt.Test.Name }
            };
            return Ok(attemptsGetByIdDto);
        }

        [HttpPost]
        [Authorize(Policy = BefitClaimConstants.HasEvaluationPermission)]
        public async Task<IActionResult> Create(AttemptCreateDto attemptCreateDto)
        {
            await _attemptService.CreateAsync(new Attempt()
            {
                Score = attemptCreateDto.Score,
                Date = attemptCreateDto.Date, 
            }, attemptCreateDto.LectorId, attemptCreateDto.StudentId, attemptCreateDto.TestId);
            return Ok("Created");
        }
        
        [Authorize(Policy = BefitClaimConstants.HasEvaluationPermission)]
        [HttpPut]
        public async Task<IActionResult> Update(AttemptUpdateDto attemptUpdateDto)
        {
            await _attemptService.UpdateAsync(attemptUpdateDto.Id, new Attempt()
            {
                Id = attemptUpdateDto.Id,
                Date = attemptUpdateDto.Date,
                Score = attemptUpdateDto.Score,

            }, attemptUpdateDto.LectorId, attemptUpdateDto.StudentId, attemptUpdateDto.TestId
            );
            return Ok("Updated");
        }
        
        [Authorize(Policy = BefitClaimConstants.HasEvaluationPermission)]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remove(string id)
        {
            await _attemptService.RemoveAsync(id);
            return Ok("Removed");
        }
    }
}
