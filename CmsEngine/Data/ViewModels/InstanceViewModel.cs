using System.Collections.Generic;

namespace CmsEngine.Data.ViewModels
{
    public class InstanceViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Culture { get; set; }
        public string UrlFormat { get; set; }
        public string DateFormat { get; set; }
        public string SiteUrl { get; set; }
        public int ArticleLimit { get; set; }
        public ContactDetailsViewModel ContactDetails { get; set; }
        public PostViewModel SelectedPost { get; set; }

        public PaginatedList<PostViewModel> PagedPosts { get; set; }
        public IEnumerable<PostViewModel> LatestPosts { get; set; }
        public IEnumerable<PageViewModel> Pages { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
