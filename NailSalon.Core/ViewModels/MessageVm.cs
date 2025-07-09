using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class MessageVm
    {
        public int MasterId { get; set; }

        [Required]
        public string Question { get; set; }

        public string? Reply { get; set; }
        public DateTime SentAt { get; set; }
    }

}
