using System.Collections.Generic;

namespace EmployeeAccounting.UI.Model
{
    public class Department
    {
        public int ID { get; set; }
        public string Signature { get; set; }
        public int? ParentID { get; set; }
        public bool IsDeleted { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}