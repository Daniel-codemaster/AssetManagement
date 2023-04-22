using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Config.Pages.Offices
{
    public class DetailsModel : SysPageModel
    {
        public Office Office { get; set; }
        public async Task OnGet(Guid id)
        {
            Office = await Db.Offices.Include(c => c.Station).FirstAsync(c => c.Id == id);
            PageTitle = Title = Office.Name;
            BreadCrumb.Add(Office.Name);
            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
        }
    }
}
