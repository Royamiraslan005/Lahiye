using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class OrderVm
    {
        public DateTime Date { get; set; }
        public string MasterName { get; set; }
        public string DesignName { get; set; }
        public decimal Price { get; set; }
    }
}
