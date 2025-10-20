using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.BLL.DTOS;
using MedicalCenter.DAL.Entities;

namespace MedicalCenter.BLL.Interfaces
{
    public interface IUserService
    {

        // get User by ID
        Task<UserOutputDTO> getUserById(int Id);

        // Get All User 
        Task<IEnumerable<UserOutputDTO>> GetAllUsersAsync();

        // Add New User
        Task<UserOutputDTO> AddNewUserAsync(UserInputDTO userInput);

        // Delete User by id
        Task<bool> deleteUserByIdAsync(int id);

        // Delete user by National Id
        Task<bool> deleteUserByNationalIdAsync(string NationalId);

        // Search For User 
        Task<IEnumerable<UserOutputDTO>> SearchUserAsync(string Keyword);

        // deActive User
        Task<bool> deActiveUser(string NationalId);

        Task <bool> ActivateUser(string NationalId);
    }
}
