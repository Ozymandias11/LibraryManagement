namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class BookCopyLogsViewModel
    {
        public required string State {  get; set; }
        public required string Message { get; set; }
        public required int QuantityModified {  get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
