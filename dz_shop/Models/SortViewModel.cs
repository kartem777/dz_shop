namespace dz_shop.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState PriceSort { get; set; }
        public SortState CompanySort { get; set; }
        public SortState DepartmentSort { get; set; }
        public SortState Current { get; set; }
        public bool Up { get; set; }
        public SortViewModel(SortState sortState)
        {
            NameSort = SortState.NameAsc;
            PriceSort = SortState.PriceAsc;
            CompanySort = SortState.CompanyAsc;
            DepartmentSort = SortState.DepartmentAsc;
            Up = true;

            if (sortState == SortState.NameDesc || sortState == SortState.PriceDesc || sortState == SortState.CompanyDesc || sortState == SortState.DepartmentDesc)
            {
                Up = false;
            }
            switch (sortState)
            {
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;

                case SortState.PriceAsc:
                    Current = PriceSort = SortState.PriceDesc;
                    break;

                case SortState.PriceDesc:
                    Current = PriceSort = SortState.PriceAsc;
                    break;

                case SortState.CompanyAsc:
                    Current = CompanySort = SortState.CompanyDesc;
                    break;

                case SortState.CompanyDesc:
                    Current = CompanySort = SortState.CompanyAsc;
                    break;

                case SortState.DepartmentAsc:
                    Current = DepartmentSort = SortState.DepartmentDesc;
                    break;

                case SortState.DepartmentDesc:
                    Current = DepartmentSort = SortState.DepartmentAsc;
                    break;

                default:
                    Current = NameSort = SortState.NameDesc;
                    break;
            }
        }
    }
}
