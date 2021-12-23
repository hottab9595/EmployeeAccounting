using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAccounting.Db.Model
{
    public class CourseEmployee : BaseModel
    {
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Employee Employee { get; set; }
    }
}