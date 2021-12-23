using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAccounting.Db.Model
{
    public class Department : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Signature { get; set; }

        [ForeignKey("Department")]
        [Column(name: "ParentID")]
        public int? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Department Parent { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}