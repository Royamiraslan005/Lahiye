using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class ProfileVm
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Zodiac { get; set; }
        public string ZodiacSymbol { get; set; }
        public string ZodiacTrait { get; set; }
        public string SuggestedDesign { get; set; }

        public List<MenuItemVm> MenuItems { get; set; } = new();
        public List<OrderVm> Orders { get; set; } = new();
        public List<ReservationInfoVm> Reservations { get; set; } = new();
        public bool ShowBirthdayDiscount { get; set; }
        public string BirthdayMessage { get; set; }

    }



}
