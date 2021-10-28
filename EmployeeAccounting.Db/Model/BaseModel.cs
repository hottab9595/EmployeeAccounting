using System.ComponentModel.DataAnnotations;

namespace EmployeeAccounting.Db.Model
{
    public abstract class BaseModel
    {
        [Key]
        public int ID { get; set; }
    }
}