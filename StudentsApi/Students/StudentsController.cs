using Microsoft.AspNetCore.Mvc;

namespace StudentsApi.Students;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetStudent()
    {
        var students = StudentsRepository.GetAll();
        return Ok(students);
    }
    
}