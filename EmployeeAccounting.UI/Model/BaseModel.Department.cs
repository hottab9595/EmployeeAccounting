using System.Collections.Generic;

namespace EmployeeAccounting.UI.Model
{
    public class Department : BaseModel
    {
        public string Signature { get; set; }
        public int? ParentID { get; set; }
        public bool IsDeleted { get; set; }
        public List<Employee> Employees { get; set; }
    }
}