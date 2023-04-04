
using AssetManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using wCyber.Helpers.Identity;
using wCyber.Helpers.Web;
using wCyber.Lib.FileStorage;

namespace AssetManagement.Web.Pages
{
    [Authorize]
    public class SysPageModel : PageModel
    {
        static SysPageModel()
        {
            PageActionBar.Styles[PageActionBar.PageActionType.PRIMARY] = "btn btn-success";
        }
        public SysPageModel()
        {
            BreadCrumb = new BreadCrumb(GetType());
        }
        IdentityClient _identityClient;
        protected IdentityClient IdentityClient
        {
            get
            {
                if (_identityClient == null)
                    _identityClient = Request.HttpContext.RequestServices.GetService<IdentityClient>();
                return _identityClient;
            }
        }

        static bool IsDbCreated;
        AssetManagementContext _db = null!;
        public AssetManagementContext Db
        {
            get
            {
                if (_db == null)
                {
                    _db = Request.HttpContext.RequestServices.GetService<AssetManagementContext>();
                    if (!IsDbCreated)
                    {
                        try
                        {
                            _db.Database.Migrate();
                        }
                        catch { }
                        IsDbCreated = true;
                    }
                }
                return _db;
            }
        }
        public Guid CurrentUserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        User _currentUser;
        public User CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = Db.Users
                        .FirstOrDefault(c => c.Id == CurrentUserId);
                }
                return _currentUser;
            }
        }

        IFileStore _filestore;
        protected IFileStore FileStore
        {
            get
            {
                if (_filestore == null) _filestore = Request.HttpContext.RequestServices.GetService<IFileStore>();
                return _filestore;
            }
        }

        [ViewData]
        public PageActionBar ActionBar { get; } = new PageActionBar();

        [ViewData]
        public string Title { get; protected set; }

        [ViewData]
        public string PageTitle { get; protected set; }
        [ViewData]
        public dynamic? PageSubTitle { get; protected set; }

        [ViewData]
        public BreadCrumb BreadCrumb { get; protected set; }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var area = context.RouteData.Values["area"]?.ToString();
            Title = GetType().Namespace[(GetType().Namespace.LastIndexOf(".") + 1)..];
            if (Title == "Pages" && GetType().Namespace.Contains("Area")) Title = area;
            base.OnPageHandlerExecuting(context);
        }
    }
}