using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAccounting.Db.Model
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        public string Signature { get; set; }

        [ForeignKey("Department")]
        public int? ParentID { get; set; }
        public bool IsDeleted { get; set; }
        public Department Parent { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}