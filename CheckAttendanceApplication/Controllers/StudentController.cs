using BusinessObjects.Models;
using DataAccess.Dao;
using Microsoft.AspNetCore.Mvc;

namespace CheckAttendanceApplication.Controllers;

public class StudentController : Controller
{
    private readonly StudentDao _studentDao;
    
    private readonly UniversityDbContext _universityDb = new();

    // GET
    public StudentController()
    {
        _studentDao = new StudentDao(_universityDb);
    }

    public IActionResult Index()
    {
        var students = _studentDao.GetAllStudents(); 
        return View(students);
    }
}