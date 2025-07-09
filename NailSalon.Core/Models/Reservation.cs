using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Reservation
    {
        public int? Id { get; set; }
        public string? AppUserId { get; set; }

        public AppUser? AppUser { get; set; }
        public int? MasterId { get; set; }

        public Master? Master { get; set; }
        public int? NailTypeId { get; set; }

        public NailType? NailType { get; set; }
        [Required(ErrorMessage = "Tarix və saat seçmək mütləqdir.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        //public TimeSpan Time { get; set; }
        public bool WantsFoodDrink { get; set; }
        public string? SelectedMenuIds { get; set; }
        public List<MenuItem>? MenuItems { get; set; }

    }

}

