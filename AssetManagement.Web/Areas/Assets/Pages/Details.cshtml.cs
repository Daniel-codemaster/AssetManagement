using AssetManagement.Data;
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
        public async Task OnGet(Guid id)
        {
            Asset = Db.Assets
                .Include(c => c.Station)
                .Include(c => c.Category)
                .Include(c => c.Creator)
                .Include(c => c.AssetAttributes)
                .FirstOrDefault(c => c.Id == id);

            if (Asset.ServiceCycleId != null)
            {
                ServiceCycle = await Db.ServiceCycles.FirstAsync(c => c.Id == Asset.ServiceCycleId);
            }
            var query = Db.Services.Where(c => c.AssetId == Asset.Id);
            if (query.Any())
            {
                LastService = query.OrderByDescending(c => c.ServiceDate).ToList().First();
            }


            string base64Image = Convert.ToBase64String(Asset.ImageData);
            ImageSrc = string.Format("data:image/png;base64,{0}", base64Image);


            PageTitle = Title = Asset.Name;
            BreadCrumb.Add(Asset.Name);
            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
        }
    }
}
