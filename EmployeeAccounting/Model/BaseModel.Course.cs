using Newtonsoft.Json;

namespace EmployeeAccounting.Model
{
    public class Course : BaseModel
    {
        public string Signature { get; set; }
        public int Duration { get; set; }
    }
}