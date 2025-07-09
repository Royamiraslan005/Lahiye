using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }
        public string Zodiac { get; set; }
    }

}
