using BeFit.MongoDb.Api.DTOs.Request;
using BeFit.MongoDb.Api.DTOs.Response;
using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.MongoDb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAsync();
            var studentsGetAllDto = new StudentGetAllDto();
            studentsGetAllDto.Items = students.Select(s => new StudentGetByIdDto { 
                Id = s.Id, 
                Name = s.Name, 
                CurrentSemester = s.CurrentSemester,
                Gender = s.Gender,
                HasEvaluationPermission = s.HasEvaluationPermission,
                Height = s.Height,
                Weight = s.Weight,
                BirthDate = s.Dob,
                Email = s.Email,
                FamilyName = s.FamilyName,
            });
            return Ok(studentsGetAllDto);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var student = await _studentService.GetAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            var studentGetByIdDto = new StudentGetByIdDto()
            {
                Id = student.Id,
                Name = student.Name,
                CurrentSemester = student.CurrentSemester,
                Gender = student.Gender,
                HasEvaluationPermission = student.HasEvaluationPermission,
                Height = student.Height,
                Weight = student.Weight,
                BirthDate = student.Dob,
                Email = student.Email,
                FamilyName = student.FamilyName
            };
            return Ok(studentGetByIdDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateDto studentCreateDto)
        {
            await _studentService.CreateAsync(new Student()
            {
                Name = studentCreateDto.Name,
                CurrentSemester = studentCreateDto.CurrentSemester,
                Gender = studentCreateDto.Gender,
                Dob = studentCreateDto.BirthDate,
                Email = studentCreateDto.Email,
                FamilyName = studentCreateDto.FamilyName,
            });
            return Ok("Created");
        }
        [HttpPut]
        public async Task<IActionResult> Update(StudentUpdateDto studentUpdateDto)
        {
            await _studentService.UpdateAsync(studentUpdateDto.Id, new Student()
            {
                Id = studentUpdateDto.Id,
                Name = studentUpdateDto.Name,
                CurrentSemester = studentUpdateDto.CurrentSemester,
                Gender = studentUpdateDto.Gender,
                HasEvaluationPermission = studentUpdateDto.HasEvaluationPermission,
                Height = studentUpdateDto.Height,
                Weight = studentUpdateDto.Weight,
                FamilyName = studentUpdateDto.FamilyName
            });
            return Ok("Updated");
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(string id)
        {
            await _studentService.RemoveAsync(id);
            return Ok("Removed");
        }
    }
}
