using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Config.Pages.Categories
{
    public class DetailsModel : SysPageModel
    {
        public AssetCategory Category { get; set; }
        public void OnGet(Guid id)
        {
            Category = Db.AssetCategories.FirstOrDefault(c => c.Id == id);
            PageTitle = Title = Category.Name;
            BreadCrumb.Add(Category.Name);
            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
        }
    }
}
