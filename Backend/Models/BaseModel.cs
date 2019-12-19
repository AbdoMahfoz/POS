using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class IgnoreInHelpersAttribute : Attribute { }
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [IgnoreInHelpers]
        public DateTime AddedDate { get; set; }
        [IgnoreInHelpers]
        public DateTime? ModifiedDate { get; set; }
        [IgnoreInHelpers]
        [Required]
        public bool IsDeleted { get; set; } = false;
        [IgnoreInHelpers]
        public DateTime? DeletedDate { get; set; }
    }
}
