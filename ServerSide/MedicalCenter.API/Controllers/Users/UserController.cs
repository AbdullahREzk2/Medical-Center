using MedicalCenter.BLL.DTOS;
using MedicalCenter.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region get User By Id
        [HttpGet("getUserBy/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> getUserById(int Id)
        {
            var user = await _userService.getUserById(Id);

            if (user == null)
                return NoContent();

            return Ok(user);
        }
        #endregion

        #region Get AllUser
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetAllUsers()
        {
            var Users = await _userService.GetAllUsersAsync();

            return Ok(Users);

        }
        #endregion

        #region Add New User
        [HttpPost("AddNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddNewUser(UserInputDTO userInput)
        {
            var user = await _userService.AddNewUserAsync(userInput);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        #endregion

        #region delete User by ID
        [Authorize(Roles ="Admin")]
        [HttpDelete ("DeleteBy/{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteByUserId(int userID)
        {
            bool User = await _userService.deleteUserByIdAsync(userID);

            if (User == true)
                return Ok();

            return BadRequest();
        }

        #endregion

        #region delete User by National Id
        [Authorize(Roles ="Admin")]
        [HttpDelete ("DeleteByNationalID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> deleteByNationalID(string nationalID)
        {
            bool User = await _userService.deleteUserByNationalIdAsync(nationalID);
            if (User == true) return Ok();
            return BadRequest();
        }

        #endregion

        #region search For User
        [HttpGet("SearchForUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Search(string Keyword)
        {
            var Users = await _userService.SearchUserAsync(Keyword);
            if(Users == null)
               return BadRequest();

            return Ok(Users);
        }

        #endregion

        #region ActivateUser
        [Authorize(Roles ="Admin")]
        [HttpPut("ActivateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> activateUser(string NationalId)
        {
            bool isActivated = await _userService.ActivateUser(NationalId);

            if(isActivated == true) return Ok();

            return BadRequest();
        }
        #endregion

        #region deActivateUser
        [Authorize(Roles ="Admin")]
        [HttpPut("deActivateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> deActivateUser(string NationalId)
        {
            bool isDeactivated = await _userService.deActiveUser(NationalId);

            if (isDeactivated == true)
                return Ok();

            return BadRequest();
        }
        #endregion



    }
}
