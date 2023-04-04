using AssetManagement.Data;
using AssetManagement.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.Web.Areas.Config.Pages.Stations
{
    public class DetailsModel : SysPageModel
    {
        public Station Station { get; set; }
        public void OnGet(Guid id)
        {
            Station = Db.Stations.FirstOrDefault(c => c.Id == id);
            PageTitle = Title = Station.Name;
            BreadCrumb.Add(Station.Name);
            ActionBar.Add("Edit", page: "Edit", routeValue: new { id = id }, icon: ActionIcon.Edit);
        }
    }
}
