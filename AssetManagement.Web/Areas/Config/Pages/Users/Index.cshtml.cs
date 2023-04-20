using AssetManagement.Web.Pages;
using AssetManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace AssetManagement.Web.Areas.Config.Pages.Users
{
    public class IndexModel : SysListPageModel<User>
    {
        public void OnGet(int? p, int? ps, string? q)
        {
            var query = Db.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                QueryString = q;
                q = q.Trim().ToUpper();
                query = query.Where(c => c.Name.ToUpper().Contains(q) || c.Email.Contains(q));
            }
            List = query.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);

            SetPageTitles("CurUser");
        }
    }
}
