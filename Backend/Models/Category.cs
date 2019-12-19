using System.Collections.Generic;

namespace Backend.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public virtual IList<ItemCategory> Items { get; set; }
    }
}