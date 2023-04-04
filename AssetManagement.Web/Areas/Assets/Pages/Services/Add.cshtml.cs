using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Assets.Pages.Services
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public Service Service { get; set; }

        public async Task OnGet(Guid assetid)
        {
            var asset = await Db.Assets.FindAsync(assetid);
            BreadCrumb.Clear();
            BreadCrumb.Add("Assets", page: "../Index");
            BreadCrumb.Add(asset.Name, page: "../Details", routeValues: new {id = asset.Id});
            BreadCrumb.Add("Services", page: "./Index", routeValues: new {assetid = asset.Id});
            BreadCrumb.Add("Add");
            PageTitle = Title = "Add new service..";
        }
        public async Task<IActionResult> OnPost(Guid assetid)
        {
            var asset = await Db.Assets.FindAsync(assetid);

            Service.Id = Guid.NewGuid();
            Service.AssetId = asset.Id;

            Db.Services.Add(Service);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
