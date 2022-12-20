using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
        }

        public int InstructorId { get; set; }
        public string? InstructorFirstName { get; set; }
        public string? InstructorMidName { get; set; }
        public string? InstructorLastName { get; set; }
        public string? InstructorFullName => $"{InstructorLastName} {InstructorMidName} {InstructorFirstName}";
        public virtual ICollection<Course> Courses { get; set; }
    }
}