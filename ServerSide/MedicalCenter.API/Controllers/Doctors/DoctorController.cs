using MedicalCenter.BLL.DTOS;
using MedicalCenter.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.API.Controllers.Doctors
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        #region Get All Doctors 
        [HttpGet("GetAllDoctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAllDoctors()
        {
            var Doctors = await _doctorService.GetAllDoctors();
            return Ok(Doctors);
        }
        #endregion

        #region Add New Doctor
        [Authorize(Roles ="Admin")]
        [HttpPost("addNewDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddNewDoctor(DoctorInputDTO doctorInput)
        {
            var doctor = await _doctorService.AddNewDoctor(doctorInput);

            if (doctor == null)
                return BadRequest();

            return Ok(doctor);
                
        }
        #endregion

        #region DeleteDoctor
        [Authorize(Roles ="Admin")]
        [HttpDelete("DeleteDoctor/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> deleteDoctor(int Id)
        {
            bool isDeleted = await _doctorService.deleteDoctorById(Id);
            if (isDeleted)
                return Ok();

            return BadRequest();
        }
        #endregion

        #region Search 
        [HttpGet("SearchForDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> SearchForDoctor(string Keyword)
        {
            var Doctor = await _doctorService.SearchForDoctor(Keyword);
            return Ok(Doctor);
        }

        #endregion


    }
}
