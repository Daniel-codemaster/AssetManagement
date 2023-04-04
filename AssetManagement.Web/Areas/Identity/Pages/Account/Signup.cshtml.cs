using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wCyber.Lib;

namespace AssetManagement.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class SignupModel : SysPageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public SignupModel(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        
        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string password)
        {
           
            return RedirectToPage("/Index");
        }
    }
}
