using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class ConfirmReservationVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
