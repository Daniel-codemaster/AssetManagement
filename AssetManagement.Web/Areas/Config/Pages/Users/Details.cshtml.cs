using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Config.Pages.Users
{
    public class DetailsModel : SysPageModel
    {
        public User User { get; set; }
        public async Task OnGet(Guid id)
        {
            User = await Db.Users.FirstAsync(m => m.Id == id);
            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
            PageTitle = Title = User.Name;
            BreadCrumb.Add(User.Name);
        }
    }
}
