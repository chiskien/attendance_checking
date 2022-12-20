using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseSchedules = new HashSet<CourseSchedule>();
            Students = new HashSet<Student>();
        }

        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseDescription { get; set; }
        public int SubjectId { get; set; }
        public int? InstructorId { get; set; }

        public virtual Instructor? Instructor { get; set; }
        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
