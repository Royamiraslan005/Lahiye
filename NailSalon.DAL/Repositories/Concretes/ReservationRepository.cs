using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Concretes
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool IsSlotAvailable(int masterId, DateTime date)
        {
            return !_context.Reservations.Any(r => r.MasterId == masterId && r.Date == date);
        }


        public void CreateReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public async Task<List<Reservation>> GetAll(string id)
        {
            return await  _context.Reservations.Where(x => x.AppUserId == id).Include(x => x.AppUser).Include(x=>x.Master).Include(x=>x.NailType).Include(x=>x.MenuItems).ToListAsync();
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsSlotAvailableAsync(int masterId, DateTime requestedDate)
        {
            var existingReservations = await _context.Reservations
                .Where(r => r.MasterId == masterId)
                .ToListAsync();

            foreach (var res in existingReservations)
            {
                var timeDiff = Math.Abs((res.Date - requestedDate).TotalMinutes);
                if (timeDiff < 90) // 1.5 saatdan azdırsa
                {
                    return false;
                }
            }

            return true;
        }




    }



}
