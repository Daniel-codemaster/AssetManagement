using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Config.Pages.Categories
{
    public class EditModel : SysPageModel
    {
        [BindProperty]
        public AssetCategory Category { get; set; }
        public async Task OnGet(Guid id)
        {
            Category = await Db.AssetCategories.FirstAsync(c => c.Id == id);
            BreadCrumb.Add(Category.Name, page: "./Details", routeValues: new { Category.Id });
            BreadCrumb.Add("Edit");
            Title = PageTitle = Category.Name;
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            Category = await Db.AssetCategories.FirstAsync(c => c.Id == id);
            await TryUpdateModelAsync(Category, "", c => c.Name);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Category.Id });
        }
    }
}
