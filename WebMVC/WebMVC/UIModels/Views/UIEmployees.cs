using WebMVC.Models;
namespace WebMVC.UIModels
{
    public class UIEmployees
    {
        public UIEmployees()
        {
            Employees = new List<Employee>();
        }
        public List<Employee> Employees { get; set; }
    }
}
