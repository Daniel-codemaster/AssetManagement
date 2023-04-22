using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetManagement.Web.Areas.Config.Pages.Offices
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public Office Office { get; set; }
        [ViewData]
        public SelectList Stations { get; set; }
        public void OnGet()
        {
            Stations = new SelectList(Db.Stations, nameof(Station.Id), nameof(Station.Name));
            BreadCrumb.Add("Add");
            PageTitle = Title = "Add new..";
        }
        public async Task<IActionResult> OnPost()
        {
            Office.Id = Guid.NewGuid();
            Db.Offices.Add(Office);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Office.Id });
        }
    }
}
