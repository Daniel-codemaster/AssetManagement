using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Config.Pages.Users
{
    public class EditModel : SysPageModel
    {
        [BindProperty]
        public User User { get; set; }
        public async Task OnGet(Guid id)
        {
            User = await Db.Users.FirstAsync(c => c.Id == id);
            BreadCrumb.Add(User.Name, page: "./Details", routeValues: new { User.Id });
            BreadCrumb.Add("Edit");
            Title = PageTitle = User.Name;
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            User = await Db.Users.FirstAsync(c => c.Id == id);
            await TryUpdateModelAsync(User, "", c => c.Name, c => c.LoginId, c => c.Email, c=>c.Mobile!);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { User.Id });
        }
    }
}
