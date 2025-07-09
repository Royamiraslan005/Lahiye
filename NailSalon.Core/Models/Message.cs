using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; } // AppUser.Id
        public string? Reply { get; set; } // Admin cavabı
        public string Question { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;

        // əlaqə
        public int MasterId { get; set; }
        public Master Master { get; set; }
    }

}
