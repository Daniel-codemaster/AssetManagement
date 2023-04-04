using Humanizer;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace AssetManagement.Web.Pages
{
    public class SysListPageModel<T> : SysPageModel where T : class
    {
        [ViewData]
        public string SearchPlaceholder { get; protected set; }
        public string QueryString { get; protected set; }
        public IPagedList<T> List { get; protected set; }
        protected int DefaultPageSize { get; set; } = 100;

        [ViewData]
        public string DateFiltersCaption { get; protected set; }

        public void SetPageTitles(string description)
        {
            Title = description.Pluralize();
            PageTitle = description.ToQuantity(List.TotalItemCount) + (string.IsNullOrWhiteSpace(QueryString) ? "" : " found..");
            if (List.PageCount > 1) PageSubTitle = new HtmlString($"Page <strong>{List.PageNumber}</strong> of <strong>{List.PageCount}</strong>");
            SearchPlaceholder = $"Search {Title}..".Humanize(LetterCasing.Sentence);
        }
    }
}