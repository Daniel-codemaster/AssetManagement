using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Assets.Pages.Attributes
{
    public class DetailsModel : SysPageModel
    {
        public AssetAttribute Attribute { get; set; }
       
        public async Task OnGet(Guid id)
        {
            Attribute = await Db.AssetAttributes
                .Include(c => c.Asset)
                .FirstAsync(c => c.Id == id);



            PageTitle = Title = Attribute.Name;

            BreadCrumb.Clear();
            BreadCrumb.Add("Assets", page: "../Index");
            BreadCrumb.Add(Attribute.Asset.Name, page: "../Details", routeValues: new { id = Attribute.Asset.Id });
            BreadCrumb.Add("Attributes", page: "../Details", routeValues: new { id = Attribute.Asset.Id });
            BreadCrumb.Add(Attribute.Name, page: ".");

            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
        }
    }
}
