using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class ReservationInfoVm
    {
        public DateTime Date { get; set; }
    
        public string? MasterName { get; set; }
        public string? DesignName { get; set; }
   
        public bool WantsMenu { get; set; }
        public string? SelectedMenuIds { get; set; }
        public List<MenuItem>? MenuItems { get; set; }
    }
}
