using System.Collections.Generic;

namespace EmployeeAccounting.Db.Model
{
    public class Course : BaseModel
    {
        public string Signature { get; set; }
        public int Duration { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}