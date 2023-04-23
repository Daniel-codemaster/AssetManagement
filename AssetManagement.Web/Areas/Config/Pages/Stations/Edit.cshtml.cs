using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Config.Pages.Stations
{
    public class EditModel : SysPageModel
    {
        [BindProperty]
        public Station Station { get; set; }
        public async Task OnGet(Guid id)
        {
            Station = await Db.Stations.FirstAsync(c => c.Id == id);
            BreadCrumb.Add(Station.Name, page: "./Details", routeValues: new { Station.Id });
            BreadCrumb.Add("Edit");
            Title = PageTitle = Station.Name;
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            Station = await Db.Stations.FirstAsync(c => c.Id == id);
            await TryUpdateModelAsync(Station, "", c => c.Name, c => c.Description!);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Station.Id });
        }
    }
}
