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
    
    [HttpGet("{indexNumber}")]
    public IActionResult GetStudent(string indexNumber)
    {
        var student = StudentsRepository.Get(indexNumber);
        return student is null ? NotFound() : Ok(student);
    }
    
}