using DomainLayer.Contracts;
using DomainLayer.Models.Identity_Module;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using Shared.DTOS.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class NurseService : INurseService
    {
        private readonly INurseRepository nurseRepository;

        public NurseService(INurseRepository nurseRepository)
        {
            this.nurseRepository = nurseRepository;
        }
        public async Task<IEnumerable<NurseDetailsDto>> GetAllNursesAsync()
        {
            var nurses = await nurseRepository.GetAllWithRelatedData();

            return nurses.Select(n => new NurseDetailsDto
            {
                Id = n.Id,
                Certification = n.Certification,
                IsAvailable = n.IsAvailable,
                PhoneNumber = n.PhoneNumber,
                UserId = n.UserId,
                FullName = n.User.FullName
            });
        }

        public async Task<NurseDetailsDto> GetNurseByIdAsync(int id)
        {
            var nurse = await nurseRepository.GetByIdWithRelatedData(id);
            if (nurse == null)
                return null;


            return new NurseDetailsDto
            {
                Id = nurse.Id,
                Certification = nurse.Certification,
                IsAvailable = nurse.IsAvailable,
                PhoneNumber = nurse.PhoneNumber,
                UserId = nurse.UserId,
                FullName = nurse.User.FullName
            };

        }
        public async Task<IEnumerable<NurseDetailsDto>> GetAvailableNursesAsync()
        {
            var nurses = await nurseRepository.GetAvailableAsync();
            return nurses.Select(n => new NurseDetailsDto
            {
                Id = n.Id,
                Certification = n.Certification,
                IsAvailable = n.IsAvailable,
                PhoneNumber = n.PhoneNumber,
                FullName = n.User.FullName,
                UserId = n.UserId
            });
        }



        public async Task UpdateNurseAsync(int id, NurseDto dto)
        {
            var nurse = await nurseRepository.GetByIdAsync(id);
            if (nurse == null) throw new Exception("Nurse not found");

            nurse.Certification = dto.Certification;
            nurse.IsAvailable = dto.IsAvailable;
            nurse.PhoneNumber = dto.PhoneNumber;
            nurse.UserId = dto.UserId;

            nurseRepository.Update(nurse);
            await nurseRepository.SaveChangesAsync();
        }



        public async Task<bool> DeleteNurseAsync(int id)
        {
            var nurse = await nurseRepository.GetByIdAsync(id);
            if (nurse != null)
            {
                nurseRepository.Delete(nurse);
                await nurseRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task ToggleAvailabilityAsync(int id, bool isAvailable)
        {
            var nurse = await nurseRepository.GetByIdAsync(id);
            if (nurse == null) throw new KeyNotFoundException("Nurse not found");

            nurse.IsAvailable = isAvailable;
            await nurseRepository.SaveChangesAsync();
        }


    }
}
