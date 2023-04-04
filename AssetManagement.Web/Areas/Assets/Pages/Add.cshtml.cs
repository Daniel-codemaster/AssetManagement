using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QRCoder;
using System.Drawing;

namespace AssetManagement.Web.Areas.Assets.Pages
{
    public class AddModel : SysPageModel
    {
        [BindProperty]
        public Asset Asset { get; set; }
        [ViewData]
        public SelectList Categories { get; set; }
        [ViewData]
        public SelectList Stations { get; set; }
        public void OnGet()
        {
            Categories = new SelectList(Db.AssetCategories, nameof(AssetCategory.Id), nameof(AssetCategory.Name));
            Stations = new SelectList(Db.Stations, nameof(Station.Id), nameof(Station.Name));
            BreadCrumb.Add("Add");
            PageTitle = Title = "Add new..";
        }
        public async Task<IActionResult> OnPost()
        {
            Asset.Id = Guid.NewGuid();
            Asset.CreatorId = CurrentUserId;
            Asset.CreationDate = DateTime.Now;
            Asset.ImageData = GenerateQRCode($"https://localhost:7084/Assets/Details/{Asset.Id}");

            Db.Assets.Add(Asset);
            await Db.SaveChangesAsync();
            return RedirectToPage("./Details", new { Asset.Id });
        }

        public static byte[] GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }
    }
}
