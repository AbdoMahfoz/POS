namespace MobileApp.Models.DataModels
{
    public class Account : BaseDataModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}