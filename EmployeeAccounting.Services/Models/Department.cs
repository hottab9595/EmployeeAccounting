namespace EmployeeAccounting.Services.Models
{
    public class Department : BaseModel
    {
        public Department(int id) : base(id)
        {
        }

        public string Signature { get; set; }
        public int? ParentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}