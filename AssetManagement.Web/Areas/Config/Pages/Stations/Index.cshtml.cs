using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace AssetManagement.Web.Areas.Config.Pages.Stations
{
    public class IndexModel : SysListPageModel<Station>
    {
        public void OnGet(int? p, int? ps, string? q)
        {
            var query = Db.Stations.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                QueryString = q;
                q = q.Trim().ToUpper();
                query = query.Where(c => c.Name.ToUpper().Contains(q));
            }

            List = query.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);

            
            ActionBar.Add("Add", page: "Add", icon: ActionIcon.Add);


            SetPageTitles("Stations");
        }
    }
}
