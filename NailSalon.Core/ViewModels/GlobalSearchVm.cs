using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.ViewModels
{
    public class GlobalSearchVm
    {
        public string SearchText { get; set; }

        public List<MasterVm> Masters { get; set; } = new();
        public List<NailTypeVm> NailTypes { get; set; } = new();
        public List<ServicesVm> Services { get; set; } = new();
    }

}
