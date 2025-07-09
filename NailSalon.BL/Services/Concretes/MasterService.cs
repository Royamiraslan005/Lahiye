using Microsoft.EntityFrameworkCore;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;

        public MasterService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }


        public async Task<List<MasterVm>> GetAllAsync()
        {
            var masters = await _masterRepository.GetAllAsync();

            return masters.Select(m => new MasterVm
            {
                Id = m.Id,
                FullName = m.FullName,
                Experience=m.Experience,
                Zodiac=m.Zodiac,
                Specialty=m.Specialty,
                ImageUrl = m.ImageUrl
            }).ToList();
        }

        public async Task<MasterVm> GetByIdAsync(int id)
        {
            var master = await _masterRepository.GetByIdAsync(id);
            if (master == null) return null;

            return new MasterVm
            {
                Id = master.Id,
                FullName = master.FullName,
                Experience=master.Experience,
                Zodiac=master.Zodiac,
                Specialty=master.Specialty,
                ImageUrl = master.ImageUrl
            };
        }

        public async Task CreateAsync(MasterVm masterVm, string wwwroot)
        {
            string? fileName = null;

            if (masterVm.formFile != null)
            {
                string folder = Path.Combine(wwwroot, "IMAGE");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                fileName = Guid.NewGuid() + Path.GetExtension(masterVm.formFile.FileName);
                string path = Path.Combine(folder, fileName);

                using FileStream stream = new FileStream(path, FileMode.Create);
                await masterVm.formFile.CopyToAsync(stream);
            }

            var master = new Master
            {
                FullName = masterVm.FullName,
                Experience = masterVm.Experience,
                Zodiac = masterVm.Zodiac,
                Specialty = masterVm.Specialty,
                ImageUrl = fileName
            };

            await _masterRepository.Create(master);
           _masterRepository.SaveAllChange(); 
        }

        public async Task UpdateAsync(MasterVm masterVm, string wwwroot)
        {
            var master = await _masterRepository.GetByIdAsync(masterVm.Id);
            if (master == null) return;
            
            master.FullName = masterVm.FullName;
            master.Experience = masterVm.Experience;
            master.Zodiac = masterVm.Zodiac;
            master.Specialty = masterVm.Specialty;

            if (masterVm.formFile != null)
            {
                string folder = Path.Combine(wwwroot, "IMAGE");
                string fileName = Guid.NewGuid() + Path.GetExtension(masterVm.formFile.FileName);
                string path = Path.Combine(folder, fileName);

                using FileStream stream = new FileStream(path, FileMode.Create);
                await masterVm.formFile.CopyToAsync(stream);

           
                if (!string.IsNullOrEmpty(master.ImageUrl))
                {
                    string oldPath = Path.Combine(folder, master.ImageUrl);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                master.ImageUrl = fileName;
            }

          await _masterRepository.UpdateAsync(master);
            _masterRepository.SaveAllChange();
        }




        public async Task DeleteAsync(int id)
        {
            var master = await _masterRepository.GetByIdAsync(id);
            if (master == null) return;

            _masterRepository.DeleteAsync(id);
            _masterRepository.SaveAllChange();
        }
     
 
    }

}
