using AssetManagement.Data;
using AssetManagement.Web.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using wCyber.Helpers.Identity;
using wCyber.Helpers.Identity.Auth;
using wCyber.Lib;
using wCyber.Lib.FileStorage;
using wCyber.Lib.FileStorage.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AssetManagementContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

//this context is for the default identity
/* services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddEntityFrameworkStores<AHCoLTContext>();*/

//customized identity using our user and role table


builder.Services.Configure<UserRoleTypeOptions>(o => o.RoleType = typeof(UserRole));

builder.Services
    .AddDefaultIdentity<User>()
    .AddUserStore<SysExternalLoginUserStore<User, UserExternalLogin, AssetManagementContext>>()
    .AddClaimsPrincipalFactory<SysExternalLoginUserStore<User, UserExternalLogin, AssetManagementContext>>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromDays(15);
    o.SlidingExpiration = true;
    o.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
    {
        OnSigningIn = async context => await context.InitUser()
    };
});

AuthHelperExtensions.ConfigureAuth(builder);
builder.Services.AddRazorPages();
if (builder.Configuration["StorageAcc:Uri"] != null)
{
    builder.Services.AddSingleton<IFileStore>(new AzureBlobStore(builder.Configuration["StorageAcc:Uri"], builder.Configuration["StorageAcc:Name"], builder.Configuration["StorageAcc:Key"]));
}
else builder.Services.AddSingleton<IFileStore>(new FSFileStore(@"c:\assetmgt\files\"));



// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddSingleton<IdentityClient>();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseMigrationsEndPoint();

app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
        response.StatusCode == (int)HttpStatusCode.Forbidden)
        response.Redirect("/Auth/AccessDenied");
    return Task.CompletedTask;
});
app.UseEndpoints((endpoints) =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.MapRazorPages();

app.Run();