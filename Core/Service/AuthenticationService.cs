using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Identity_Module;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTOS.Registeration;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,
        IDriverRepository _driverRepository, INurseRepository _nurseRepository) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new UnAuthorizedException();
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                throw new UnAuthorizedException();
            return new UserDTO
            {
                Email = user.Email,
                DisplayName = user.FullName,
                Token = await CreateTokenAsync(user),
                FullName = user.FullName,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "User"

            };

        }
        public async Task<UserDTO> DriverRegisterAsync(DriverRegisterDTO driverDto)
        {
            if (await IsEmailExistAsync(driverDto.Email))
                throw new Exception("Email already exists");

            var driverUser = new ApplicationUser
            {
                FullName = driverDto.FullName,
                UserName = driverDto.Email,
                Email = driverDto.Email,
                PhoneNumber = driverDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(driverUser, driverDto.Password);

            if (!result.Succeeded)
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(Errors);
            }

            await _userManager.AddToRoleAsync(driverUser, "Driver");

            var driver = new Driver
            {
                LicenseNumber = driverDto.LicenseNumber,
                PhoneNumber = driverDto.PhoneNumber,
                IsAvailable = driverDto.IsAvailable,
                UserId = driverUser.Id
            };
            await _driverRepository.AddAsync(driver);
            await _driverRepository.SaveChangesAsync();

            return new UserDTO
            {
                Email = driverUser.Email,
                FullName = driverUser.FullName,
                Token = await CreateTokenAsync(driverUser),
                Role = "Driver",
                DisplayName = driverUser.FullName
            };
        }

        public async Task<UserDTO> NurseRegisterAsync(NurseRegisterDTO nurseDto)
        {
            if (await IsEmailExistAsync(nurseDto.Email))
                throw new Exception("Email already exists");
            var nurseUser = new ApplicationUser
            {
                FullName = nurseDto.FullName,
                UserName = nurseDto.Email,
                Email = nurseDto.Email,
                PhoneNumber = nurseDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(nurseUser, nurseDto.Password);
            if (!result.Succeeded)
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(Errors);
            }
            await _userManager.AddToRoleAsync(nurseUser, "Nurse");
            var nurse = new Nurse
            {
                Certification = nurseDto.Certification,
                PhoneNumber = nurseDto.PhoneNumber,
                UserId = nurseUser.Id,
                IsAvailable = true
            };
            await _nurseRepository.AddAsync(nurse);
            await _nurseRepository.SaveChangesAsync();
            return new UserDTO
            {
                Email = nurseDto.Email,
                FullName = nurseDto.FullName,
                Token = await CreateTokenAsync(nurseUser),
                Role = "Nurse",
                DisplayName = nurseDto.FullName
            };
        }

        public async Task<UserDTO> PatientRegisterAsync(PatientRegisterDTO patientDto)
        {
            if (await IsEmailExistAsync(patientDto.Email))
                throw new Exception("Email already exists");
            var patientUser = new ApplicationUser
            {
                FullName = patientDto.FullName,
                UserName = patientDto.Email,
                Email = patientDto.Email,
                PhoneNumber = patientDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(patientUser, patientDto.Password);
            if (!result.Succeeded)
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(Errors);
            }
            await _userManager.AddToRoleAsync(patientUser, "Patient");
            var patient = new Patient
            {
                Address = patientDto.Address,
                MedicalHistory = patientDto.MedicalHistory,
                Gender = patientDto.Gender,
                DateOfBirth = patientDto.DateOfBirth,
                PhoneNumber = patientDto.PhoneNumber,
                UserId = patientUser.Id
            };
            return new UserDTO
            {
                Email = patientDto.Email,
                FullName = patientDto.FullName,
                Token = await CreateTokenAsync(patientUser),
                Role = "Patient",
                DisplayName = patientUser.FullName
            };

        }
        public async Task<UserDTO> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null!;

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            return new UserDTO
            {
                Email = user.Email!,
                FullName = user.FullName,
                DisplayName = user.FullName,
                Role = role,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c792cb12ebb7bd6599b42f326d504d16a59379aee8e14d932a39e97c20d1e5fb"));
            var siginCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: "https://localhost:7159/", audience: "My auidience", claims, DateTime.UtcNow.AddDays(30), signingCredentials: siginCreds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
