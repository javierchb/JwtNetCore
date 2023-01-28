namespace WebMVC.UIModels
{
    public class UIUser
    {
        public UIUser()
        {
            this.Username = "";
            ViewEmployees = new UIEmployees();
        }
        public int InSesion { get; set; }                     
        public string Username { get; set; }
        public UIEmployees ViewEmployees { get; set; }
        

    }
}
