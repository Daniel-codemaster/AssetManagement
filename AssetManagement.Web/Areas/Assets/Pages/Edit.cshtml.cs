using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Assets.Pages
{
    public class EditModel : SysPageModel
    {
        [BindProperty]
        public Asset Asset { get; set; }
        [ViewData]
        public SelectList Categories { get; set; }
        [ViewData]
        public SelectList Stations { get; set; }
        [ViewData]
        public SelectList Offices { get; set; }
        public async Task OnGet(Guid id)
        {
            Asset = await Db.Assets.FirstAsync(c => c.Id == id);
            Categories = new SelectList(Db.AssetCategories, nameof(AssetCategory.Id), nameof(AssetCategory.Name));
            Stations = new SelectList(Db.Stations, nameof(Station.Id), nameof(Station.Name));
            Offices = new SelectList(Db.Offices, nameof(Office.Id), nameof(Office.Name));
            BreadCrumb.Add(Asset.Name, page: "./Details", routeValues: new { Asset.Id});
            BreadCrumb.Add("Edit");
            Title = PageTitle = Asset.Name;
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            Asset = await Db.Assets.FirstAsync(c => c.Id == id);
            await TryUpdateModelAsync(Asset, "", c => c.Name, c => c.Name, c => c.Make!, c => c.OfficeId!, c=>c.CategoryId,c=>c.StationId, c=>c.Description!, c=> c.SerialNumber!);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Asset.Id });
        }
    }
}
