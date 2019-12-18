using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User : BaseModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
    }
}
