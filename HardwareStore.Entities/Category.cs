using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Entities
{
    public class Category : EntityBase
    {
        [StringLength(100)]
        public string Name { get; set; } = default!;
        public string? ImageUrl { get; set; }

    }
}