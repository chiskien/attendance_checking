using BusinessObjects.Models;
using DataAccess.Dao;
using Microsoft.AspNetCore.Mvc;

namespace CheckAttendanceApplication.Controllers;

public class HomeController : Controller
{
    private readonly InstructorDao _instructorDao;

    private readonly UniversityDbContext _dbContext = new UniversityDbContext();

    // GET
    public HomeController()
    {
        _instructorDao = new InstructorDao(_dbContext);
    }

    public IActionResult Index()
    {
        var listInstructor = _instructorDao.GetAllInstructors();
        return View(listInstructor);
    }
}