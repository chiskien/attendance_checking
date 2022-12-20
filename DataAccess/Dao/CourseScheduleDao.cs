using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao;

public class CourseScheduleDao
{
    private readonly UniversityDbContext _context;

    public CourseScheduleDao(UniversityDbContext context)
    {
        _context = context;
    }

    public List<CourseSchedule> CourseSchedulesOfCourse(int courseId)
    {
        var listSchedules = _context.CourseSchedules
            .Include(cs => cs.Course)
            .ThenInclude(c => c.Subject)
            .Include(c => c.Course)
            .ThenInclude(c => c.Instructor)
            .Where(cs => cs.CourseId == courseId).ToList();
        return listSchedules;
    }

    public CourseSchedule GetCourseSchedule(int courseScheduleId)
    {
        CourseSchedule schedule;
        try
        {
            schedule = _context.CourseSchedules
                .Include(c => c.Course)
                .ThenInclude(c => c.Students)
                .Include(course => course.Course.Subject)
                .Include(c => c.RollCallBooks)
                .FirstOrDefault(x => x.TeachingScheduleId == courseScheduleId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return schedule;
    }
}