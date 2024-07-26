using Library.Data.RequestFeatures;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class PagedViewModel<T> : MetaDataViewModel
    {
        public IEnumerable<T> Items { get; set; }

        public PagedViewModel(IEnumerable<T> items, MetaData metaData)
        {
            Items = items;
            MetaData = metaData;
        }
    }
}
