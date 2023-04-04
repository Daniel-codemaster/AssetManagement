﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AssetManagement.Web.Pages;
using AssetManagement.Data;
using AssetManagement.Web.Areas.Identity.Pages.Account;
using AssetManagement.Lib;

namespace AssetManagement.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    
    public class ActivateModel : SysPageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public ActivateModel( UserManager<User> userManager, ILogger<LoginModel> logger)
        {
            _logger = logger;
            _userManager = userManager;
        }
        public bool IsActivated { get; private set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(Guid code)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            Title = PageTitle = "Activate login account";
            
            var user = await Db.Users.FindAsync(code);
          //  IsActivated = user.ActivationDate.HasValue;
            Input = new InputModel { Email = user.Email };
        }

        #region snippet
       
        #endregion
    }
}
