using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Data
{
    public partial class Service
    {
        [NotMapped]
        public string ServiceDateString
        {
            get => ServiceDate.ToString();
            set => ServiceDate = DateOnly.Parse(value);
        }
    }
}
