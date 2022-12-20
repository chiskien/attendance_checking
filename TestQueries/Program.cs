// See https://aka.ms/new-console-template for more information

using BusinessObjects.Models;
using DataAccess.Dao;

namespace TestQueries
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (UniversityDbContext context = new UniversityDbContext())
            {
                RollCallBookDao rollCallBookDao = new RollCallBookDao(context);
                CourseDao courseDao = new CourseDao(context);
                CourseScheduleDao courseScheduleDao = new CourseScheduleDao(context);
                InstructorDao instructorDao = new InstructorDao(context);

                var listInstructor = instructorDao.GetAllInstructors();
                
            }
        }
    }
}