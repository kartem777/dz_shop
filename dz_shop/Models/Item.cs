using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace dz_shop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
