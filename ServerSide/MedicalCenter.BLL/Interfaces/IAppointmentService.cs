using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.BLL.DTOS;
using MedicalCenter.DAL.Enums;

namespace MedicalCenter.BLL.Interfaces
{
    public interface IAppointmentService
    {

        // Add New appointment
        Task<AppointmentOutputDTO> addNewappointment(AppointmentInputDTO appointmentInput);

        //GetAllAppointments
        Task<IEnumerable<AppointmentOutputDTO>> getAllAppointments();

        //GetAppointmentById
        Task<AppointmentOutputDTO> getAppointmentById(int Id);

        //GetAppointmentsByDoctorId
        Task<IEnumerable<AppointmentOutputDTO>> GetAppointmentsByDoctorId(int doctorId);

        //GetAppointmentsByPatient
        Task<IEnumerable<AppointmentOutputDTO>> GetAppointmentsByPatientId(int patientId);

        //DeleteAppointment
        Task<bool> DeleteAppointment(int appointmentId);

        //UpdateAppointmentStatus

































        Task<bool> updateAppointmentStatus(int appointmentId,string newStatus);

    }
}
