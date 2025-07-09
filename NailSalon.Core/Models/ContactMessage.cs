using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
        public string? Reply { get; set; }             // Admin cavabı
        public bool IsReplied { get; set; } = false;   // Cavab verilib/verilməyib


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
