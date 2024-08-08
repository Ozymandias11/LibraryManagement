namespace LibraryManagement.ViewModels.Reports
{
    public class GenericRangeReportViewModel
    {
        public required string Name { get; set; }
        public int Total { get; set; }
        public string TotalLabel { get; set; } = "Total";
    }
}
