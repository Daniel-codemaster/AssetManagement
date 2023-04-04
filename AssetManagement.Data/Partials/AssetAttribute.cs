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
       
    }
}
