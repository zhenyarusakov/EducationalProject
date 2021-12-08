using System.Threading.Tasks;
using EC.Infrastructure.Abstractions;
using EC.Infrastructure.DTO.Student;
using EC.Infrastructure.DTO.Subjects;
using Microsoft.AspNetCore.Mvc;

namespace EducationalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentDataService _service;

        public StudentController(IStudentDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest student)
        {
            var result = await _service.CreateStudentAsync(student);

            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> SetGrade([FromBody] SetGradeRequest request)
        {
            await _service.SetGradeAsync(request);

            return Ok();
        }

        [HttpGet("avg")]
        public async Task<IActionResult> GetStudentsByAvgGradeAscending()
        {
            var result = await _service.GetStudentsByAvgGradeAscendingAsync();

            return Ok(result);
        }

        [HttpGet("excellent")]
        public async Task<IActionResult> GetExcellentStudents()
        {
            var result = await _service.GetExcellentStudentsAsync();

            return Ok(result);
        }
    }
}