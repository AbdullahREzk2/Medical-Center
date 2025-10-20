using MedicalCenter.BLL.DTOS;
using MedicalCenter.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.API.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PateintsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PateintsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        #region Get All Pateints 
        [HttpGet("GetAllPatients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAllPatients()
        {
            var Pateints = await _patientService.GetAllPateints();

            return Ok(Pateints);
        }

        #endregion

        #region Add New Pateint
        [Authorize(Roles = "Admin,Reception")]
        [HttpPost("AddNewPateint")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddNewPateint(PatientInputDTO patientInput)
        {
            var Pateint = await _patientService.AddNewPateint(patientInput);

            return Ok(Pateint);
        }
        #endregion

        #region Delete Patient by Id
        [Authorize(Roles = "Admin,Reception")]
        [HttpDelete("DeleteBy/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> deleteUserById(int Id)
        {
            var isDeleted = await _patientService.deletePateintwithId(Id);

            if (isDeleted)
                return Ok();

            return BadRequest();
        }
        #endregion

        #region Search 
        [HttpGet("SearchPateints")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search(string keyword)
        {
            var patients = await _patientService.SearchPateints(keyword);

            return Ok(patients);
        }
        #endregion

    }
}
