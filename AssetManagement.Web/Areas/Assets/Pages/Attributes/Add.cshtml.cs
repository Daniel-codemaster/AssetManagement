using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Assets.Pages.Attributes
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public AssetAttribute AssetAttribute { get; set; }
       
        public async Task OnGet(Guid assetid)
        {
           var asset = await Db.Assets.FindAsync(assetid);
            BreadCrumb.Clear();
            BreadCrumb.Add("Assets");
            BreadCrumb.Add(asset.Name);
            BreadCrumb.Add("Attributes");
            BreadCrumb.Add("Add");
            PageTitle = Title = "Add new..";
        }
        public async Task<IActionResult> OnPost(Guid assetid)
        {
            var asset = await Db.Assets.FindAsync(assetid);

            AssetAttribute.Id = Guid.NewGuid();
            AssetAttribute.AssetId = asset.Id;
           

            Db.AssetAttributes.Add(AssetAttribute);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = AssetAttribute.Id });
        }
    }
}
