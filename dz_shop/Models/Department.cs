using System.ComponentModel.DataAnnotations;
namespace dz_shop.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<Item> Items { get; set; } = new();
    }
}
