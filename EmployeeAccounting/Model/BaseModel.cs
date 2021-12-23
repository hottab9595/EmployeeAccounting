using Newtonsoft.Json;

namespace EmployeeAccounting.Model
{
    public abstract class BaseModel
    {
        [JsonProperty(PropertyName = "ID")]
        public int Id { get; set; }
    }
}