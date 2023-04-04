using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Config.Pages.Stations
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public Station Station { get; set; }
        public void OnGet()
        {
            BreadCrumb.Add("Add");
            PageTitle = Title = "Add new..";
        }
        public async Task<IActionResult> OnPost()
        {
            Station.Id = Guid.NewGuid();
            Db.Stations.Add(Station);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Station.Id });
        }
    }
}
