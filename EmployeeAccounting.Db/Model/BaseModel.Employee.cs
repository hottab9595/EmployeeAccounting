using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAccounting.Db.Model
{
    public class Employee : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Patronymic { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Department")]
        [Column(name: "DepartmentID")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<CourseEmployee> CourseEmployees { get; set; }
    }
}