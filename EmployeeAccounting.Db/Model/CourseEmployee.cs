using System.Collections.Generic;

namespace EmployeeAccounting.Db.Model
{
    public class CourseEmployee
    {
        public int CourseId { get; set; }
        public int EmployeeId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Employee Employee { get; set; }
    }
}