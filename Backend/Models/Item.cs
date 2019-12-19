using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Item : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Base64Image { get; set; }
        public int Stock { get; set; } = 1;
        public virtual IList<ItemCategory> Categories { get; set; }
    }
}