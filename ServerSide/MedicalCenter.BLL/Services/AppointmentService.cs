using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedicalCenter.BLL.DTOS;
using MedicalCenter.BLL.Interfaces;
using MedicalCenter.DAL.Context;
using MedicalCenter.DAL.Entities;
using MedicalCenter.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalCenter.BLL.Services
{
    public class AppointmentService: IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly MedicalCenterDbContext _dbContext;

        public AppointmentService(IMapper mapper , MedicalCenterDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<AppointmentOutputDTO> addNewappointment(AppointmentInputDTO appointmentInput)
        {

         using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var doctor = await _dbContext.Doctors.FindAsync(appointmentInput.DoctorID);
            if (doctor == null)
                throw new Exception("Doctor Not found ! ");

            var pateint = await _dbContext.Patients.FindAsync(appointmentInput.PatientID);
            if (pateint == null)
                throw new Exception("Patient Not Found !");

            bool isOverLapping = await _dbContext.Appointments
                .AnyAsync(a => a.DoctorID == appointmentInput.DoctorID &&
                a.AppointmentDate == appointmentInput.AppointmentDate);

            if (isOverLapping)
                throw new Exception("Doctor Already has an appointment at this time !");

            var appointment = _mapper.Map<Appointment>(appointmentInput);
            appointment.Status = AppointmentStatusEnum.Pending;

            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();  
            await transaction.CommitAsync();

            var createdAppointment = await _dbContext.Appointments
                .Include(a=>a.Doctor)
                .Include(a=>a.Patient)
                .FirstOrDefaultAsync(a=>a.AppointmentID == appointment.AppointmentID);

            return _mapper.Map<AppointmentOutputDTO>(createdAppointment);
          
        }


        public async Task<IEnumerable<AppointmentOutputDTO>> getAllAppointments()
        {
            var appointments = await _dbContext.Appointments
                .Include(a=>a.Doctor)
                .Include(a=>a.Patient)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentOutputDTO>>(appointments);
        }

        public async Task<AppointmentOutputDTO> getAppointmentById(int Id)
        {
            var appointment = await _dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a=>a.AppointmentID ==  Id);

            return _mapper.Map<AppointmentOutputDTO>(appointment); 
        }

        public async Task<IEnumerable<AppointmentOutputDTO>> GetAppointmentsByDoctorId(int doctorId)
        {
            var appointments = await _dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.DoctorID == doctorId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentOutputDTO>>(appointments);
        }

        public async Task<IEnumerable<AppointmentOutputDTO>> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _dbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientID == patientId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentOutputDTO>>(appointments);
        }

        public async Task<bool> DeleteAppointment(int appointmentId)
        {
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);

            if (appointment == null)
                return false;

             _dbContext.Appointments.Remove(appointment);
             await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> updateAppointmentStatus(int appointmentId ,string Status)
        {
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);

            if(appointment == null)
                return false;

            if(Enum.TryParse<AppointmentStatusEnum>(Status,true,out var newStatus))
            {
                appointment.Status = newStatus;
                appointment.UpdatedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
