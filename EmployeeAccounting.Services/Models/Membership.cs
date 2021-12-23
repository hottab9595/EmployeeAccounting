namespace EmployeeAccounting.Services.Models
{
    public class Membership : BaseModel
    {
        public Membership(int id) : base(id)
        {
        }

        public Employee Employee { get; set; }
        public Course Course { get; set; }
    }
}