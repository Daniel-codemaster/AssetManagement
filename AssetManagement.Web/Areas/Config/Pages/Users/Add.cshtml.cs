using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wCyber.Lib;

namespace AssetManagement.Web.Areas.Config.Pages.Users
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {
            BreadCrumb.Add("Add");
            PageTitle = Title = "Add new..";
        }
        public async Task<IActionResult> OnPost()
        {
            
            User.Id = Guid.NewGuid();
            User.CreationDate = DateTime.UtcNow;
            User.SecurityStamp = Guid.NewGuid().ToString();
            User.IsActive = false;
            User.IsEmailConfirmed = false;
            User.RoleId = (int)UserRole._User;
            

            Db.Users.Add(User);
           
            await Db.SaveChangesAsync();

            return RedirectToPage("./Details", new { User.Id });
        }
    }
}
