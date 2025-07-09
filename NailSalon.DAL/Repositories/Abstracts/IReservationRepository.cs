using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IReservationRepository
    {
        Task<bool> IsSlotAvailableAsync(int masterId, DateTime requestedDate);


        Task<List<Reservation>> GetAll(string id);
        Task AddAsync(Reservation reservation);
        Task CreateAsync(Reservation reservation);
    }

}
