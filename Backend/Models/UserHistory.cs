using Backend.DataContracts;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class UserHistory : BaseModel
    {
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int Count { get; set; } = 1;
        public bool IsPurchased { get; set; } = false;
        public PurchaseMethod PurchaseMethod { get; set; }
    }
}