using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Assets.Pages.Attributes
{
    public class EditModel : SysPageModel
    {
        [BindProperty]
        public AssetAttribute AssetAttribute { get; set; }

        public async Task OnGet(Guid id)
        {
            AssetAttribute = await Db.AssetAttributes
                .Include(c => c.Asset)
                .FirstAsync(c => c.Id == id);

            BreadCrumb.Clear();
            BreadCrumb.Add("Assets", page: "../Index");
            BreadCrumb.Add(AssetAttribute.Asset.Name, page: "../Details", routeValues: new { id = AssetAttribute.AssetId });
            BreadCrumb.Add("Attributes");
            BreadCrumb.Add(AssetAttribute.Asset.Name, page: "./Details", routeValues: new {id = AssetAttribute.Id});
            BreadCrumb.Add("Edit");
            PageTitle = Title = AssetAttribute.Name;
        }
        public async Task<IActionResult> OnPost(Guid id)
        {

            var attribute = await Db.AssetAttributes.FirstAsync(c => c.Id == id);
            attribute.Name = AssetAttribute.Name;
            attribute.ExpiryDate = AssetAttribute.ExpiryDate;

            Db.Update(attribute);
            
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = AssetAttribute.Id });
        }
    }
}
