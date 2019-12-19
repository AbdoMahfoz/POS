
namespace Backend.Models
{
    public class ItemCategory : BaseModel
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}