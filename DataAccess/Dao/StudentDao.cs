using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao;

public class StudentDao
{
    private readonly UniversityDbContext _context;

    public StudentDao(UniversityDbContext context)
    {
        _context = context;
    }

    public List<Student> GetAllStudents()
    {
        return _context.Students.ToList();
    }

    public List<Student> GetAllStudentByCourse(int courseId)
    {
        return _context.Courses
            .Include(c => c.Students)
            .Include(c => c.CourseSchedules)
            .ThenInclude(c => c.RollCallBooks)
            .First(c => c.CourseId == courseId).Students.ToList();
    }
}