using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Web.Areas.Assets.Pages
{
    public class DownloadQRModel : SysPageModel
    {
        public string ImageSrc {get;set;}
        public async Task OnGet(Guid id)
        {
            var asset = await Db.Assets.FirstAsync(c => c.Id == id);
            string base64Image = Convert.ToBase64String(asset.ImageData);
            ImageSrc = string.Format("data:image/png;base64,{0}", base64Image);
        }
    }
}
