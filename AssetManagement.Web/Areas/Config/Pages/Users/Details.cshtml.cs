using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Config.Pages.Users
{
    public class DetailsModel : SysPageModel
    {
        public User User { get; set; }
        public void OnGet()
        {
            User = Db.Users.FirstOrDefault(m => m.Id == CurrentUserId);

            PageTitle = Title = User.Name;

        }
    }
}
