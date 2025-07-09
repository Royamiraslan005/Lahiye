using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class SaleVm
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string StripeSessionId { get; set; }
    }
}
