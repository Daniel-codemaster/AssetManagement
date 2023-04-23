using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Config.Pages.Offices
{
    public class EditModel : SysPageModel
    {
        [BindProperty]
        public Office Office { get; set; }
        [ViewData]
        public SelectList Stations { get; set; }
        public async Task OnGet(Guid id)
        {
            Office = await Db.Offices.FirstAsync(c => c.Id == id);
            Stations = new SelectList(Db.Stations, nameof(Station.Id), nameof(Station.Name));
            BreadCrumb.Add(Office.Name, page: "./Details", routeValues: new { Office.Id });
            BreadCrumb.Add("Edit");
            Title = PageTitle = Office.Name;
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            Office = await Db.Offices.FirstAsync(c => c.Id == id);
            await TryUpdateModelAsync(Office, "", c => c.Name, c=>c.StationId!);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Office.Id });
        }
    }
}
