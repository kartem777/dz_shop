using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dz_shop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dz_shop.Controllers
{
    public class HomeController : Controller
    {
        ItemsContext db;
        public HomeController(ItemsContext db)
        {
            this.db = db;
            if (!db.Companies.Any())
            {
                Company c1 = new Company() { Name = "Pass Extreme" };
                Company c2 = new Company() { Name = "Pass+" };
                Company c3 = new Company() { Name = "Drive+" };
                Company c4 = new Company() { Name = "Fast driving" };
                Company c5 = new Company() { Name = "International Office" };
                Company c6 = new Company() { Name = "Cheap Ink" };

                Department d1 = new Department() { Name = "UA Pass" };
                Department d2 = new Department() { Name = "Driving licence" };
                Department d3 = new Department() { Name = "International documents" };

                Item i1 = new Item() { Name = "Pass", Price = 4000, Company = c1, Department = d1 };
                Item i2 = new Item() { Name = "Pass Fast", Price = 6000, Company = c1, Department = d1 };
                Item i3 = new Item() { Name = "Pass VIP", Price = 8000, Company = c1, Department = d1 };
                Item i4 = new Item() { Name = "Pass Diamond", Price = 16000, Company = c1, Department = d1 };
                Item i5 = new Item() { Name = "Pass", Price = 2000, Company = c2, Department = d1 };
                Item i6 = new Item() { Name = "Pass+", Price = 3000, Company = c2, Department = d1 };
                Item i7 = new Item() { Name = "Pass++", Price = 4000, Company = c2, Department = d1 };

                Item i8 = new Item() { Name = "Drive", Price = 4000, Company = c4, Department = d2 };
                Item i9 = new Item() { Name = "Drive Fast", Price = 6000, Company = c4, Department = d2 };
                Item i10 = new Item() { Name = "Drive VIP", Price = 8000, Company = c4, Department = d2 };
                Item i11 = new Item() { Name = "Drive Diamond", Price = 16000, Company = c4, Department = d2 };
                Item i12 = new Item() { Name = "Drive", Price = 2000, Company = c3, Department = d2 };
                Item i13 = new Item() { Name = "Drive+", Price = 3000, Company = c3, Department = d2 };
                Item i14 = new Item() { Name = "Drive++", Price = 4000, Company = c3, Department = d2 };

                Item i15 = new Item() { Name = "IO", Price = 4000, Company = c5, Department = d3 };
                Item i16 = new Item() { Name = "IO Fast", Price = 6000, Company = c5, Department = d3 };
                Item i17 = new Item() { Name = "IO VIP", Price = 8000, Company = c5, Department = d3 };
                Item i18 = new Item() { Name = "IO Diamond", Price = 16000, Company = c5, Department = d3 };
                Item i19 = new Item() { Name = "Cheap", Price = 2000, Company = c6, Department = d3 };
                Item i20 = new Item() { Name = "Cheap+", Price = 3000, Company = c6, Department = d3 };
                Item i21 = new Item() { Name = "Cheap++", Price = 4000, Company = c6, Department = d3 };

                db.Companies.AddRange(c1, c2, c3, c4, c5, c6);
                db.Departments.AddRange(d1, d2, d3);
                db.Items.AddRange(i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15, i16, i17, i18, i19, i20, i21);
                db.SaveChanges();
            }
        }
        public async Task<IActionResult> Index(int? department, string? name, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Item>? items = db.Items.Include(x => x.Company).Include(y => y.Department);

            if (department != null && department != 0)
            {
                items = items.Where(p => p.DepartmentId == department);
            }
            if (!string.IsNullOrEmpty(name))
            {
                items = items.Where(p => p.Name!.Contains(name));
            }
            List<Department> departments = db.Departments.ToList();
            departments.Insert(0, new Department { Name = "All", Id = 0 });

            items = sortOrder switch
            {
                SortState.NameDesc => items.OrderByDescending(s => s.Name),
                SortState.PriceAsc => items.OrderBy(s => s.Price),
                SortState.PriceDesc => items.OrderByDescending(s => s.Price),
                SortState.CompanyAsc => items.OrderBy(s => s.Company!.Name),
                SortState.CompanyDesc => items.OrderByDescending(s => s.Company!.Name),
                SortState.DepartmentAsc => items.OrderBy(s => s.Department!.Name),
                SortState.DepartmentDesc => items.OrderByDescending(s => s.Department!.Name),
                _ => items.OrderBy(s => s.Name)
            };
            IndexViewModel viewModel = new IndexViewModel()
            {
                Items = await items.AsNoTracking().ToListAsync(),
                Departments = new SelectList(departments, "Id", "Name", department),
                Name = name,
                SortViewModel = new SortViewModel(sortOrder)
            };

            return View(viewModel);
        }
    }
}
