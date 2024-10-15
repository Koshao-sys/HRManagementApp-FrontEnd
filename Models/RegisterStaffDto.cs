namespace HRManagementAppFrontEnd.Models
{
    public class RegisterStaffDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string OtherNames { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdPhoto { get; set; }
        public string AccessCode { get; set; }
        public string EmployeeNumber { get; set; }
    }
}
