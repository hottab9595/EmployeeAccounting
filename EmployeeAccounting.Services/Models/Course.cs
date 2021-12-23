namespace EmployeeAccounting.Services.Models
{
    public class Course : BaseModel
    {
        public Course(int id) : base(id)
        {
        }

        public string Signature { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
    }
}