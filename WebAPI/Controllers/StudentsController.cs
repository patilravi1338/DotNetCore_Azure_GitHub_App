using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
  private readonly StudentService _studentService;

  public StudentsController(StudentService studentService)
  {
    _studentService = studentService;
  }

  [HttpGet(Name = "GetStudents")]
  public async Task<IActionResult> GetStudents()
  {
    try
    {
      var students = await _studentService.GetStudentsAsync();
      return Ok(students);
    }catch (Exception ex)
    {
      return StatusCode(500, ex);
    }
  }

  [HttpPost]
  public async Task<IActionResult> AddStudent([FromBody] Student student)
  {
    await _studentService.AddStudentAsync(student);
    return Ok();
  }
}
