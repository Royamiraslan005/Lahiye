using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Models
{
    public class NailType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }
        public string? Zodiac { get; set; }
    }

}
