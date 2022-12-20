using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao;

public class CourseDao
{
    private readonly UniversityDbContext _context;

    public CourseDao(UniversityDbContext context)
    {
        _context = context;
    }

    public List<Course> GetAllCoursesOfInstructor(int instructorId)
    {
        List<Course> courses = _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Subject)
            .Where(c => c.InstructorId == instructorId)
            .OrderBy(c => c.CourseCode)
            .ThenBy(c => c.Subject.SubjectCode)
            .ToList();

        return courses;
    }

    public Course? GetCourseById(int courseId)
    {
        return _context.Courses.Find(courseId);
    }
}