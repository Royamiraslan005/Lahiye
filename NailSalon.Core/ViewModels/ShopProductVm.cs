using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class ShopProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Kateqoriya seçilməlidir")]
        public string? Category { get; set; }
        public IFormFile? FormFile { get; set; }
        public int LikeCount { get; set; }
        public bool IsLikedByUser { get; set; }
    }
}
