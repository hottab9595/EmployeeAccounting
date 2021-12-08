using System.Collections.Generic;

namespace EmployeeAccounting.UI.Model
{
    public class Employee : BaseModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public bool IsDeleted { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentSignature { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}