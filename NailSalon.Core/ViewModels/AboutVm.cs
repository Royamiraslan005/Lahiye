﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class AboutVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public List<FeatureVm> Features { get; set; }
    }

   
}
