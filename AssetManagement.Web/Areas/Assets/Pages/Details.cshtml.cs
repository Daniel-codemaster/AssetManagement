using AssetManagement.Data;
using AssetManagement.Lib;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Assets.Pages
{
    public class DetailsModel : SysPageModel
    {
        public Asset Asset { get; set; }

        public ServiceCycle ServiceCycle { get; set; }

        public Service LastService { get; set; }
        public string ImageSrc { get; set; }
        public bool IsDueForService = false;
        public async Task OnGet(Guid id)
        {
            Asset = await Db.Assets
                .Include(c => c.Station)
                .Include(c => c.Category)
                .Include(c => c.Creator)
                .Include(c => c.AssetAttributes)
                .Include(c => c.Office)
                .FirstAsync(c => c.Id == id);

            if (Asset.ServiceCycleId != null)
            {
                ServiceCycle = await Db.ServiceCycles.FirstAsync(c => c.Id == Asset.ServiceCycleId);
            }
            var query = Db.Services.Where(c => c.AssetId == Asset.Id);
            if (query.Any())
            {
                LastService = query.OrderByDescending(c => c.ServiceDate).ToList().First();

                if(ServiceCycle!= null)
                {
                    if(ServiceCycle.TypeId == (int)CycleType.Day)
                    {
                        if(DateOnly.FromDateTime(DateTime.Now) > LastService.ServiceDate.AddDays(ServiceCycle.Period))
                        {
                            IsDueForService = true;
                        }
                    }
                    else if (ServiceCycle.TypeId == (int)CycleType.Month)
                    {
                        if (DateOnly.FromDateTime(DateTime.Now) > LastService.ServiceDate.AddMonths(ServiceCycle.Period))
                        {
                            IsDueForService = true;
                        }
                    }
                    else
                    {
                        if (DateOnly.FromDateTime(DateTime.Now) > LastService.ServiceDate.AddYears(ServiceCycle.Period))
                        {
                            IsDueForService = true;
                        }
                    }
                }
            }


            string base64Image = Convert.ToBase64String(Asset.ImageData);
            ImageSrc = string.Format("data:image/png;base64,{0}", base64Image);


            PageTitle = Title = Asset.Name;
            BreadCrumb.Add(Asset.Name);
            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
        }
    }
}
