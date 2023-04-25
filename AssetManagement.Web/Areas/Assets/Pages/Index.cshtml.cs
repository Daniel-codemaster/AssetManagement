using AssetManagement.Data;
using AssetManagement.Lib;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AssetManagement.Web.Areas.Assets.Pages
{
    public class IndexModel : SysListPageModel<Asset>
    {
        public async Task OnGet(int? p, int? ps, string? q, int? dueForService, int? expLicenses)
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

            if (dueForService != null)
            {
                QueryString = "Due for service";
                List<Asset> assets = new List<Asset>();
                
                foreach (var asset in query.ToList())
                {
                    var services = Db.Services
                        .Include(c => c.Asset)
                        .Where(c => c.AssetId == asset.Id)
                        .OrderByDescending(c => c.ServiceDate)
                        .ToList();

                    if (services.Any())
                    {
                        var service = services[0];
                        if (asset.ServiceCycleId != null)
                        {
                            var cycle = await Db.ServiceCycles.FirstAsync(c => c.Id == asset.ServiceCycleId);

                            DateOnly date;
                            if (cycle.TypeId == (int)CycleType.Day)
                            {
                                date = service.ServiceDate.AddDays(cycle.Period);
                            }
                            else if (cycle.TypeId == (int)CycleType.Month)
                            {
                                date = service.ServiceDate.AddMonths(cycle.Period);
                            }
                            else
                            {
                                date = service.ServiceDate.AddYears(cycle.Period);
                            }


                            if (DateOnly.FromDateTime(DateTime.Now) > date)
                            {
                                assets.Add(asset);
                            }
                        }

                    }

                }
                List = assets.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);
            }
            if(expLicenses != null)
            {
                List<Asset> assets = new List<Asset>();
                var assetAttributes = Db.AssetAttributes
                    .Include(a => a.Asset.Category)
                    .Include(a => a.Asset.Station)
                    .ToList();

                assetAttributes = assetAttributes.Where(c => c.Expired == true).ToList();
                assets = assetAttributes.Select(c => c.Asset).ToList();
                List = assets.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);
            }
            if(List == null)
            {
                List = query.OrderBy(c => c.Name).ToPagedList(p ?? 1, ps ?? DefaultPageSize);
            }
            


            ActionBar.Add("Add", page: "Add", icon: ActionIcon.Add);


            SetPageTitles("Assets");
        }
    }
}
