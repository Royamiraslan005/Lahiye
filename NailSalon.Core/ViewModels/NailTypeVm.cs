using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class NailTypeVm
    {
        public int Id { get; set; }

        public string Title { get; set; }
        [Required(ErrorMessage = "Qiymət boş ola bilməz.")]
        [Range(0.01, 999.99, ErrorMessage = "Qiymət düzgün deyil.")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Display(Name = "Şəkil seçin")]
        public IFormFile? FormFile { get; set; }
        public string? Zodiac { get; set; }
    }

}
