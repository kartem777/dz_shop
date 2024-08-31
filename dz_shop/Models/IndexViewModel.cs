using Microsoft.AspNetCore.Mvc.Rendering;

namespace dz_shop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        public SortViewModel SortViewModel { get; set; } = new SortViewModel(SortState.NameAsc);
        public SelectList Departments { get; set; } = new SelectList(new List<Department>(), "Id", "Name");
        public string? Name { get; set; }
    }
}
