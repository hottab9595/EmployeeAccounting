using Newtonsoft.Json;

namespace EmployeeAccounting.Model
{
    public class Membership : BaseModel
    {
        public Employee Employee { get; set; }

        public Course Course { get; set; }
    }
}