using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IReservationService
    {
        Task<bool> MakeReservationAsync(ReservationVm reservation);
        List<Design> GetDesignsByZodiac(string zodiacSign);
        Task CreateAsync(Reservation reservation);
        Task<List<ReservationInfoVm>> GetUpcomingReservationsAsync(string userId);
        Task<List<ReservationInfoVm>> GetPastReservationsAsync(string userId);



        Task<List<ReservationInfoVm>> GetAll(string id);
    }
}

