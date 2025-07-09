using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Master
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Experience { get; set; }
        public string Zodiac { get; set; }
        public string Specialty { get; set; }
        public string ImageUrl { get; set; }
        public double DurationHours { get; set; }



        public ICollection<Reservation> Reservations { get; set; }
    }
}
