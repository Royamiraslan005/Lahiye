using NailSalon.Core.ViewModels.NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IContactService
    {
        Task CreateAsync(ContactVm vm);
    }
}
