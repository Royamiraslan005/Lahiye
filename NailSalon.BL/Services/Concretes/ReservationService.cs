using Microsoft.AspNetCore.Identity;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Helpers;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using NailSalon.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IUserService _userService;
        private readonly IMasterService _masterService;
        private readonly IMenuRepository _menuRepo;
        private readonly UserManager<AppUser> _userManager;
        public ReservationService(IReservationRepository repository, IUserService userService, IMasterService masterService, IMenuRepository menuRepo, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userService = userService;
            _masterService = masterService;
            _menuRepo = menuRepo;
            _userManager = userManager;
        }

        public async Task<bool> MakeReservationAsync(ReservationVm reservationvm)
        {
            if(reservationvm.MasterId==null || reservationvm.MasterId == 0)
            {
                return false;
            }
            if (reservationvm.Date < DateTime.Now)
                return false;

            // Yeni async metodla yoxla
            var isAvailable = await _repository.IsSlotAvailableAsync(reservationvm.MasterId, reservationvm.Date);

            if (!isAvailable)
                return false;

            var menuItems = _menuRepo.GetByIdsAsync(reservationvm.SelectedMenuIds);

            var reservation = new Reservation()
            {
                Date = reservationvm.Date,
                WantsFoodDrink = reservationvm.WantsFoodDrink,
                MasterId = reservationvm.MasterId,
                NailTypeId = reservationvm.DesignId,
                AppUserId = reservationvm.UserId,
                MenuItems = menuItems

            };
            var user =await _userManager.FindByIdAsync(reservationvm.UserId);
            string body = $@"
<!DOCTYPE html>
<html lang=""az"">
<head>
    <meta charset=""UTF-8"">
    <title>Zodiac Nails Rezarvasiya Təsdiqi</title>
</head>
<body style=""font-family:'Segoe UI', sans-serif; background-color:#fff8fb; color:#4a4a4a; padding:20px;"">
    <div style=""max-width:600px; margin:0 auto; background:#ffffff; border-radius:10px; box-shadow:0 0 10px rgba(255, 105, 180, 0.1); padding:30px;"">
        <h2 style=""color:#d63384; text-align:center;"">💅 Sizin Rezarvasiya Təsdiqləndi!</h2>
        <p>Salam, gözəllik ulduzumuz ⭐</p>
        <p>
            Zodiac Nails ailəsinə xoş gəlmisiniz! 💖<br>
            Sizin <strong>rezervasiyanız uğurla qeydə alındı</strong> və sizi səbirsizliklə gözləyirik! 🌸
        </p>

        <table style=""width:100%; margin-top:20px; border-collapse:collapse;"">
            <tr>
                <td style=""padding:8px 0;""><strong>📅 Tarix:</strong></td>
                <td style=""padding:8px 0;"">{reservationvm.Date:dd.MM.yyyy HH:mm}</td>
            </tr>
            <tr>
                <td style=""padding:8px 0;""><strong>☕ Menyu:</strong></td>
                <td style=""padding:8px 0;"">{string.Join(", ", menuItems.Select(x => x.Name))}</td>
            </tr>
        </table>

        <p style=""margin-top:20px;"">
            Əlavə sualınız olarsa, biz buradayıq – sizi hər zaman dinləməyə hazırıq 💌<br>
            <strong>Vaxtında gəlməyi unutmayın</strong>, çünki gözəllik zamana aşiqdir ✨
        </p>

        <p style=""margin-top:30px;"">
            Sevgilərlə,<br>
            <strong>Zodiac Nails</strong> komandası 💅🌙
        </p>

        <hr style=""margin:30px 0; border:none; border-top:1px solid #ffe0ec;"">
        <p style=""text-align:center; font-size:14px; color:#999;"">
            📲 <a href=""https://instagram.com/zodiacnails"" style=""color:#d63384; text-decoration:none;"">Instagram</a> |
            📞 <a href=""https://wa.me/994XXXXXXXXX"" style=""color:#d63384; text-decoration:none;"">WhatsApp</a> |
            🌐 <a href=""https://zodiacnails.az"" style=""color:#d63384; text-decoration:none;"">Websitemiz</a>
        </p>

        <p style=""font-size:13px; color:#b0a7ad; text-align:center;"">
            P.S. Bürcünüzə uyğun seçilmiş dizayn sizi valeh edəcək! 🔮
        </p>
    </div>
</body>
</html>";

            var result = EmailService.SendEmail(user.Email, "reservasiya", body);

            await _repository.CreateAsync(reservation);
            return true;
            }

        public List<Design> GetDesignsByZodiac(string zodiacSign)
        {

            return _userService.GetAllDesigns().Where(d => d.Zodiac == zodiacSign || d.Zodiac == "All").ToList();
        }


        public async Task<List<ReservationInfoVm>> GetAll(string id)
        {

            var reservations = await _repository.GetAll(id);

            var masters = _masterService.GetAllAsync();

            var vms = reservations.Select(x => new ReservationInfoVm()
            {
                MasterName = x.Master?.FullName,
                DesignName = x.NailType?.Title,
                WantsMenu = x.WantsFoodDrink,
                Date = x.Date,
                MenuItems = x.MenuItems,
            }).ToList();

            return vms;
        }


        public async Task CreateAsync(Reservation reservation)
        {
            await _repository.CreateAsync(reservation);
        }

        public async Task<List<ReservationInfoVm>> GetUpcomingReservationsAsync(string userId)
        {
            var reservations = await _repository.GetAll(userId);
            return reservations
                .Where(x => x.Date >= DateTime.Now)
                .Select(x => new ReservationInfoVm
                {
                    MasterName = x.Master.FullName,
                    DesignName = x.NailType.Title,
                    WantsMenu = x.WantsFoodDrink,
                    Date = x.Date,
                    MenuItems = x.MenuItems
                }).ToList();
        }

        public async Task<List<ReservationInfoVm>> GetPastReservationsAsync(string userId)
        {
            var reservations = await _repository.GetAll(userId);
            return reservations
                .Where(x => x.Date < DateTime.Now)
                .Select(x => new ReservationInfoVm
                {
                    MasterName = x.Master?.FullName,
                    DesignName = x.NailType?.Title,
                    WantsMenu = x.WantsFoodDrink,
                    Date = x.Date,
                    MenuItems = x.MenuItems
                }).ToList();
        }


    }
}
