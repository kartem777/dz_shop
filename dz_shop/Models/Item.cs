using System.ComponentModel.DataAnnotations;
namespace dz_shop.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(0, 1000000)]
        public int Price { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
