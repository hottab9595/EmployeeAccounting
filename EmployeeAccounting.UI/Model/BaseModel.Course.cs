using System.Collections.Generic;

namespace EmployeeAccounting.UI.Model
{
    public class Course : BaseModel
    {
        public string Signature { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}