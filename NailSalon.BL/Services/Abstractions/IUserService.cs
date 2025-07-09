using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IUserService
    {
        User GetUserByUsername(string username);
        string CalculateZodiac(DateTime birthDate);
        List<Design> GetAllDesigns();
        List<Master> GetAllMasters();
    }

}
