using CmsEngine.Attributes;

namespace CmsEngine.Data.ViewModels.DataTableViewModels
{
    public class WebsiteTableViewModel : BaseViewModel, IViewModel
    {
        [Searchable, Orderable, ShowOnDataTable(0)]
        public string Name { get; set; }

        [Searchable, Orderable, ShowOnDataTable(1)]
        public string Description { get; set; }

        [Searchable, Orderable, ShowOnDataTable(2)]
        public string Culture { get; set; }

        [Searchable, Orderable, ShowOnDataTable(3)]
        public string UrlFormat { get; set; } = $"{Constants.SiteUrl}/{Constants.Type}/{Constants.Slug}";

        [Searchable, Orderable, ShowOnDataTable(4)]
        public string DateFormat { get; set; } = "MM/dd/yyyy";

        [Searchable, Orderable, ShowOnDataTable(5)]
        public string SiteUrl { get; set; }
    }
}