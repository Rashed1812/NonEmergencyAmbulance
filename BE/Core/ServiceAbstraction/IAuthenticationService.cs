using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.Registeration;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        #region Register All Users
        //Register a new Driver
        Task<UserDTO> DriverRegisterAsync(DriverRegisterDTO driverDto);
        //Register a new Nurse
        Task<UserDTO> NurseRegisterAsync(NurseRegisterDTO nurseDto);
        //Register a new Patient
        Task<UserDTO> PatientRegisterAsync(PatientRegisterDTO patientDto);
        #endregion

        //Login End Point
        Task<UserDTO> LoginAsync(LoginDTO loginDto);

        //Logout End Point
        Task LogoutAsync();

        //Get Current User
        Task<UserDTO> GetCurrentUserAsync(string email);
        //Check Email
        Task<bool> IsEmailExistAsync(string email);
    }
}
