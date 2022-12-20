using BusinessObjects.Models;
using DataAccess.Dao;
using Microsoft.AspNetCore.Mvc;

namespace CheckAttendanceApplication.Controllers;

public class CourseController : Controller
{
    private readonly UniversityDbContext _universityDbContext = new();
    private readonly CourseDao _courseDao;
    private readonly InstructorDao _instructorDao;

    // GET
    public CourseController()
    {
        _courseDao = new CourseDao(_universityDbContext);
        _instructorDao = new InstructorDao(_universityDbContext);
    }

    public IActionResult GetAllCoursesOfInstructor(int id)
    {
        List<Course> courses = _courseDao.GetAllCoursesOfInstructor(id);
        Instructor instructor = _instructorDao.GetInstructorById(id);
        ViewBag.Instructor = instructor;
        return View(courses);
    }
}