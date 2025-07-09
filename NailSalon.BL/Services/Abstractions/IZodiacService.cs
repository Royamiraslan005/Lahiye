using NailSalon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IZodiacService
    {
        string Calculate(DateTime birthDate);
        ZodiacInfo GetZodiacInfo(DateTime birthDate);
    }
}
