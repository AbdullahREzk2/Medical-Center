using MedicalCenter.BLL.DTOS;
using MedicalCenter.BLL.Interfaces;
using MedicalCenter.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.API.Controllers.Appointments
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        #region Add New appointment
        [Authorize(Roles = "Admin,Reception")]
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task <IActionResult> AddNewAppointment(AppointmentInputDTO appointmentInput)
        {
            var appointment = await _appointmentService.addNewappointment(appointmentInput);
            return Ok(appointment);
            
        }
        #endregion

        #region Get All Appointments 
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> getAllAppointments()
        {
            var appointments = await _appointmentService.getAllAppointments();
            return Ok(appointments);
        }

        #endregion

        #region Get Appointment By Id
        [HttpGet("getBy/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> getappointmentById(int Id)
        {
            var appointment = await _appointmentService.getAppointmentById(Id);
            return Ok(appointment);
        }
        #endregion

        #region Get Doctor Appointments
        [HttpGet("getDoctorAppointments/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> getDoctorAppointments(int doctorId)
        {
            var appointments = await _appointmentService.GetAppointmentsByDoctorId(doctorId);
            return Ok(appointments);
        }
        #endregion

        #region Get Patient Appointments
        [HttpGet("getPatientAppointments/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> getPatientAppointments(int patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatientId(patientId);
            return Ok(appointments);
        }
        #endregion

        #region delete appointment
        [Authorize(Roles = "Admin,Reception")]
        [HttpDelete("deleteAppointment/{appointmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> deleteAppointment(int appointmentId)
        {
            bool isDeleted = await _appointmentService.DeleteAppointment(appointmentId);

            if (isDeleted)
                return Ok();

            return BadRequest();
        }

        #endregion

        #region Update Appointment Status
        [Authorize(Roles = "Admin,Reception")]
        [HttpPut("updateStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> updateStatus(int appointmentId, string status)
        {
            bool isUpdated = await _appointmentService.updateAppointmentStatus(appointmentId, status);

            if(isUpdated)
                return Ok();

            return BadRequest();
        }

        #endregion


    }
}
