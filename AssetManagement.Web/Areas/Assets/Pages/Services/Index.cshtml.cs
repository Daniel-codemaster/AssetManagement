using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace AssetManagement.Web.Areas.Assets.Pages.Services
{
    public class IndexModel : SysListPageModel<Service>
    {
        public async Task OnGet(Guid assetid, int? p, int? ps, string? q)
        {
            var query = Db.Services.AsQueryable();

            var asset = await Db.Assets.FindAsync(assetid);

            if (!string.IsNullOrWhiteSpace(q))
            {
                QueryString = q;
                q = q.Trim().ToUpper();
                query = query.Where(c => c.Description!.ToUpper().Contains(q) );
            }

            List = query.OrderByDescending(c => c.ServiceDate).ToPagedList(p ?? 1, ps ?? DefaultPageSize);


            ActionBar.Add("Add", page: "Add", icon: ActionIcon.Add, routeValue: new { assetid = asset.Id }) ;

            BreadCrumb.Clear();
            BreadCrumb.Add("Assets", page: "../Index");
            BreadCrumb.Add(asset.Name, page: "../Details", routeValues: new { id = asset.Id });
            BreadCrumb.Add("Services");
            SetPageTitles("Service history");
        }
    }
}
