using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class MenuItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        [Display(Name = "Şəkil seçin")]
        public IFormFile? FormFile { get; set; }
        public bool IsSelected { get; set; }
    }

}
