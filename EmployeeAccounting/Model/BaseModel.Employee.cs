using Newtonsoft.Json;

namespace EmployeeAccounting.Model
{
    public class Employee : BaseModel
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentSignature { get; set; }
    }
}