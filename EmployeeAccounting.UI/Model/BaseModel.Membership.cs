using System.Collections.Generic;

namespace EmployeeAccounting.UI.Model
{
    public class Membership : BaseModel
    {
        public List<Employee> Employees = new List<Employee>();
        public List<Course> Courses = new List<Course>();
    }
}