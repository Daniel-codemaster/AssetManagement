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
            BreadCrumb.Add(Attribute.Name);
            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
        }
    }
}
