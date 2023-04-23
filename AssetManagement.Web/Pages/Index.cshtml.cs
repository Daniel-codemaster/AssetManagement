using AssetManagement.Data;
using AssetManagement.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Pages
{
    public class IndexModel : SysPageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Asset> Assets = new List<Asset>();
        public List<AssetAttribute> AssetAttributes = new List<AssetAttribute>();
        public int DueForService = 0;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
           
            AssetAttributes = Db.AssetAttributes.ToList();
            AssetAttributes = AssetAttributes.Where(c => c.Expired == true).ToList();

            var assets = Db.Assets.ToList();
            Assets = assets;
            foreach(var asset in assets)
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
                        if(cycle.TypeId == (int)CycleType.Day)
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
                            DueForService += 1;
                        }
                    }
                    
                }
               
            }
        }
        public async Task<IActionResult> OnPostAsset(string q)
        {
           
            var asset = Db.Assets.FirstOrDefault(c => c.Number == q || c.SerialNumber == q);

            if(asset != null)
            {
                return RedirectToPage("/Details", new { area="Assets", asset.Id });
            }
            return Page();
            
            
        }
    }
}