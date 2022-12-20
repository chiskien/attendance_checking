using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }

        public int StudentId { get; set; }
        public string Roll { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = null!;
        public string? FullName => $"{LastName} {MiddleName} {FirstName}";
        public string? Image { get; set; }

        public virtual List<RollCallBook> RollCallBooks { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}