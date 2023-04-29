
using AssetManagement.Data;
using DinkToPdf;
using DinkToPdf.Contracts;
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
        public byte[] GeneratePdf(string link)
        {
            var PdfConverter = Request.HttpContext.RequestServices.GetService<IConverter>();
            var cookies = new Dictionary<string, string>(Request.Cookies);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A5,
                    Margins = new MarginSettings() { Top = 10, Bottom = 10 },

                },
                Objects = {
                    new ObjectSettings()
                    {
                        Page = link,
                        LoadSettings = new LoadSettings{ Cookies= cookies},
                        FooterSettings = new FooterSettings
                        {
                            Left = "Page [page] of [toPage]",
                            FontSize = 9,
                            Spacing = 10,
                        },
                    },
                }
            };
            return PdfConverter.Convert(doc);
        }
    }
}