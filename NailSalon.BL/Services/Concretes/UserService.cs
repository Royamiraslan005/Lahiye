using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public UserService(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public string CalculateZodiac(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

            return (month, day) switch
            {
                (1, <= 19) => "Oğlaq",
                (1, _) => "Dolça",
                (2, <= 18) => "Dolça",
                (2, _) => "Balıq",
                (3, <= 20) => "Balıq",
                (3, _) => "Qoç",
                (4, <= 19) => "Qoç",
                (4, _) => "Buğa",
                (5, <= 20) => "Buğa",
                (5, _) => "Əkizlər",
                (6, <= 20) => "Əkizlər",
                (6, _) => "Xərçəng",
                (7, <= 22) => "Xərçəng",
                (7, _) => "Şir",
                (8, <= 22) => "Şir",
                (8, _) => "Qız",
                (9, <= 22) => "Qız",
                (9, _) => "Tərəzi",
                (10, <= 22) => "Tərəzi",
                (10, _) => "Əqrəb",
                (11, <= 21) => "Əqrəb",
                (11, _) => "Oxatan",
                (12, <= 21) => "Oxatan",
                (12, _) => "Oğlaq",
                _ => "Naməlum"
            };
        }

        public List<Design> GetAllDesigns()
        {
            return _context.Designs.ToList();
        }

        public List<Master> GetAllMasters()
        {
            return _context.Masters.ToList();
        }

       
    }

}
