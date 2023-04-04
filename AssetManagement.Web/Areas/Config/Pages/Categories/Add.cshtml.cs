using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Config.Pages.Categories
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public AssetCategory Category { get; set; }
        public void OnGet()
        {
            BreadCrumb.Add("Add");
            PageTitle = Title = "Add new..";
        }
        public async Task<IActionResult> OnPost()
        {
            Category.Id = Guid.NewGuid();
            Db.AssetCategories.Add(Category);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Category.Id });
        }
    }
}
