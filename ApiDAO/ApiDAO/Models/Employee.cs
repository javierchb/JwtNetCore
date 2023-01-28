namespace ApiDAO.Models
{
    public class Employee
    {
        public Employee()
        {
            this.NationalIDNumber = "";
            this.EmployeeName = "";
            this.LoginID = "";
            this.JobTitle = "";
            this.MaritalStatus = "";
            this.Gender = "";
        }
        public int EmployeeID { get; set; }
        public string NationalIDNumber { get; set; }
        public string EmployeeName { get; set; }
        public string LoginID { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public int VacationHours { get; set; }
        public int SickLeaveHours { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
