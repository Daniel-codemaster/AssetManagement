using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Assets.Pages
{
    public class ServiceCycleModel : SysPageModel
    {
        [BindProperty]
        public ServiceCycle ServiceCycle { get; set; }
        public async Task OnGet(Guid assetid)
        {
            var asset = await Db.Assets.FindAsync(assetid);
            BreadCrumb.Clear();
            BreadCrumb.Add("Assets", page: "./Index");
            BreadCrumb.Add(asset.Name, page: "./Details", routeValues: new { id = asset.Id });
            BreadCrumb.Add("Update service cylce");

            PageTitle = Title = "Add new cycle..";
        }

        public async Task<IActionResult> OnPost(Guid assetid)
        {
            var asset = await Db.Assets.FindAsync(assetid);
            if (asset.ServiceCycleId == null)
            {
                ServiceCycle.Id = Guid.NewGuid();
                Db.ServiceCycles.Add(ServiceCycle);
                asset.ServiceCycleId = ServiceCycle.Id;
                Db.Update(asset);
            }
            else
            {
                var serviceCycle = Db.ServiceCycles.FirstOrDefault(c => c.Id == asset.ServiceCycleId);
                serviceCycle.Period = ServiceCycle.Period;
                serviceCycle.TypeId = ServiceCycle.TypeId;
                Db.Update(serviceCycle);
            }
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = asset.Id });
        }
    }
}
