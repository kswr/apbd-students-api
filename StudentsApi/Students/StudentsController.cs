using Microsoft.AspNetCore.Mvc;
using StudentsApi.Students.Model;

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

    [HttpPut]
    public IActionResult UpdateStudent(StudentDetails student)
    {
        var updatedStudent = StudentsRepository.Update(student);
        return updatedStudent is null ? NotFound() : Ok(updatedStudent);
    }

    [HttpPost]
    public IActionResult AddStudent(StudentDetails student)
    {
        var newStudent = StudentsRepository.Add(student);
        return newStudent is null ? Conflict() : Ok(newStudent);
    }
    
}