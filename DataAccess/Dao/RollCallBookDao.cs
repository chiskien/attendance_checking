using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Dao;

public class RollCallBookDao
{
    private readonly UniversityDbContext _context;

    public RollCallBookDao(UniversityDbContext context)
    {
        _context = context;
    }

    public List<RollCallBook> GetRollCallBooksByTeachingSchedule(int teachingScheduleId)
    {
        var result = _context.RollCallBooks
            .Include(rcb => rcb.Student)
            .Include(rcb => rcb.TeachingSchedule)
            .Where(rcb => rcb.TeachingScheduleId == teachingScheduleId)
            .ToList();
        return result;
    }

    public void SaveRollCallBooks(List<RollCallBook> rollCallBooks)
    {
        try
        {
            _context.RollCallBooks.UpdateRange(rollCallBooks);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}