using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AssetManagement.Web.Areas.Config.Pages.Offices
{
    public class IndexModel : SysListPageModel<Office>
    {
        public void OnGet(int? p, int? ps, string? q)
        {
            var query = Db.Offices.Include(c => c.Station).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                QueryString = q;
                q = q.Trim().ToUpper();
                query = query.Where(c => c.Name.ToUpper().Contains(q));
            }

            List = query.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);

            
            ActionBar.Add("Add", page: "Add", icon: ActionIcon.Add);


            SetPageTitles("Office");
        }
    }
}
