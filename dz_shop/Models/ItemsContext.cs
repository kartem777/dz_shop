using Microsoft.EntityFrameworkCore;

namespace dz_shop.Models
{
    public class ItemsContext : DbContext
    {
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;

        public ItemsContext(DbContextOptions<ItemsContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
