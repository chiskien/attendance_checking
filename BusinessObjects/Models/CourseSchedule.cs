using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class CourseSchedule
    {
        public CourseSchedule()
        {
            RollCallBooks = new HashSet<RollCallBook>();
        }

        public int TeachingScheduleId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? TeachingDate { get; set; }
        public int? Slot { get; set; }
        public string? Description { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<RollCallBook> RollCallBooks { get; set; }
    }
}