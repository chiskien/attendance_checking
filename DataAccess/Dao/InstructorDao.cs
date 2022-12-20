using BusinessObjects.Models;

namespace DataAccess.Dao;

public class InstructorDao
{
    private readonly UniversityDbContext _context;

    public InstructorDao(UniversityDbContext context)
    {
        _context = context;
    }

    public Instructor GetInstructorById(int instructorId)
    {
        return _context.Instructors.First(i => i.InstructorId == instructorId);
    }

    public List<Instructor> GetAllInstructors()
    {
        return _context.Instructors.ToList();
    }
}