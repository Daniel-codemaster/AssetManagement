using AssetManagement.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Data
{
    public partial class ServiceCycle
    {
        [NotMapped]
        public CycleType Type
        {
            get => (CycleType)TypeId;
            set => TypeId = (int)value;
        }
    }
}
