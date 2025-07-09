using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class MasterVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad və Soyad boş ola bilməz.")]
        public string FullName { get; set; }
        [Required]
        public string Experience { get; set; }

        [Required]
        public string Zodiac { get; set; }

        [Required]
        public string Specialty { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]

        public IFormFile? formFile { get; set; } 
       
    }

}
