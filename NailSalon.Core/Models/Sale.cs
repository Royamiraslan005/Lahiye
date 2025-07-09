using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int ProductId { get; set; }
        public ShopProduct Product { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
