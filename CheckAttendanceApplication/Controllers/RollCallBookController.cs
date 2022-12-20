using BusinessObjects.Models;
using DataAccess.Dao;
using Microsoft.AspNetCore.Mvc;

namespace CheckAttendanceApplication.Controllers;

public class RollCallBookController : Controller
{
    private readonly StudentDao _studentDao;
    private readonly RollCallBookDao _rollCallBookDao;
    private readonly CourseScheduleDao _courseScheduleDao;

    private readonly UniversityDbContext _universityDbContext = new();

    // GET
    public RollCallBookController()
    {
        _courseScheduleDao = new CourseScheduleDao(_universityDbContext);
        _studentDao = new StudentDao(_universityDbContext);
        _rollCallBookDao = new RollCallBookDao(_universityDbContext);
    }

    public IActionResult CheckAttendance(int id) //"id" is courseScheduleId
    {
        CourseSchedule courseSchedule = _courseScheduleDao.GetCourseSchedule(id);
        Course? course = courseSchedule.Course;
        List<Student> students = _studentDao.GetAllStudentByCourse(course!.CourseId);
        ViewBag.Students = students;
        ViewBag.Course = course;
        ViewBag.CourseSchedule = courseSchedule;
        return View(courseSchedule.RollCallBooks.ToList());
    }

    public IActionResult SaveRollCallBook(List<RollCallBook> rollCallBooks, int cid)
    {
        _rollCallBookDao.SaveRollCallBooks(rollCallBooks);
        return RedirectToAction("GetCourseScheduleOfInstructor", "CourseSchedule", new { id = cid });
    }
}