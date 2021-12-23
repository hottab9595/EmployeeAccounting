using Newtonsoft.Json;

namespace EmployeeAccounting.Model
{
    public class Department : BaseModel
    {
        public string Signature { get; set; }

        [JsonProperty(PropertyName = "ParentDepartmentId")]
        public int? ParentId { get; set; }
    }
}