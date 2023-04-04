using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AssetManagement.Web.Areas.Assets.Pages
{
    public class IndexModel : SysListPageModel<Asset>
    {
        public void OnGet(int? p, int? ps, string? q)
        {
            var query = Db.Assets
                .Include(c => c.Station)
                .Include(c => c.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                QueryString = q;
                q = q.Trim().ToUpper();
                query = query.Where(c => c.Name.ToUpper().Contains(q) || c.SerialNumber.Contains(q) || c.Number.Contains(q));
            }

            List = query.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);


            ActionBar.Add("Add", page: "Add", icon: ActionIcon.Add);


            SetPageTitles("Assets");
        }
    }
}
