using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Data
{
    public partial class AssetAttribute
    {
        [NotMapped]
        public string ExpiryDateString 
        { 
            get => ExpiryDate?.ToString(); 
            set => ExpiryDate = DateOnly.Parse(value); 
        }
        [NotMapped]
        public bool Expired 
        {
            get
            {
                if (ExpiryDate == null) return false;
                if ((DateOnly.FromDateTime(DateTime.Now) > ExpiryDate)) return true;
                return false;
            }
        } 
       
    }
}
