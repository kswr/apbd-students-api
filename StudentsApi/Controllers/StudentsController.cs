using Microsoft.AspNetCore.Mvc;

namespace StudentsApi.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetStudent()
    {
        var list = new List<StudentDetails>();
        return Ok(list);
    }
    
}

public class StudentDetails
{
}