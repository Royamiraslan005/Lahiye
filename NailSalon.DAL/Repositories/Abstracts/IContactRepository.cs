using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IContactRepository
    {
        Task CreateAsync(Contact contact);
    }

}
