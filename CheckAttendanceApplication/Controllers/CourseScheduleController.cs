using BusinessObjects.Models;
using DataAccess.Dao;
using Microsoft.AspNetCore.Mvc;

namespace CheckAttendanceApplication.Controllers;

public class CourseScheduleController : Controller
{
    private readonly UniversityDbContext _dbContext = new();

    private readonly CourseScheduleDao _scheduleDao;
    private readonly CourseDao _courseDao;

    // GET
    public CourseScheduleController()
    {
        _scheduleDao = new CourseScheduleDao(_dbContext);
        _courseDao = new CourseDao(_dbContext);
    }

    public IActionResult GetCourseScheduleOfInstructor(int id) // "id" is CourseId
    {
        List<CourseSchedule> courseSchedules = _scheduleDao.CourseSchedulesOfCourse(id);
        var course = _courseDao.GetCourseById(id);
        if (course != null)
        {
            ViewBag.Course = course;
        }

        return View(courseSchedules);
    }
}