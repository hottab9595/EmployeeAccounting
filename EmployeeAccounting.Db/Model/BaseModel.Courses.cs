using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAccounting.Db.Model
{
    public class Course : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Signature { get; set; }
        [Required]
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<CourseEmployee> CourseEmployees { get; set; }
    }
}