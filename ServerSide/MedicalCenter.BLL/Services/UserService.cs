using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MedicalCenter.BLL.DTOS;
using MedicalCenter.BLL.Helpers;
using MedicalCenter.BLL.Interfaces;
using MedicalCenter.BLL.Validations;
using MedicalCenter.DAL.Context;
using MedicalCenter.DAL.Entities;
using MedicalCenter.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace MedicalCenter.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly MedicalCenterDbContext _context;

        public UserService(IMapper mapper , MedicalCenterDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserOutputDTO> getUserById(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
                return null!;

            return _mapper.Map<UserOutputDTO>(user);    
        }

        public async Task<IEnumerable<UserOutputDTO>> GetAllUsersAsync()
        {
             var Users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserOutputDTO>>(Users);
            
        }

        public async Task<UserOutputDTO> AddNewUserAsync(UserInputDTO userInput)
        {
            var validator = new UserValidator(_context);
            var result = await validator.ValidateAsync(userInput);
            if (!result.IsValid)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(errors);
            }

            var user = _mapper.Map<User>(userInput);
            user.PasswordHash = HashedPass.HashPassword(userInput.Password);
            
            _context.Users.Add(user);
           await _context.SaveChangesAsync();

            return _mapper.Map<UserOutputDTO>(user);

        }

        public async Task<bool> deleteUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
           await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> deleteUserByNationalIdAsync(string nationalId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.NationalID == nationalId);

            if (user == null)
                return false;

            if (user.Role == RoleEnum.Admin) 
                return false;

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.UserID == user.UserID);
            if (doctor != null)
            {
                var doctorAppointments = await _context.Appointments
                    .Where(a => a.DoctorID == doctor.DoctorID)
                    .ToListAsync();
                _context.Appointments.RemoveRange(doctorAppointments);
                _context.Doctors.Remove(doctor);
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.UserID == user.UserID);
            if (patient != null)
            {
                var patientAppointments = await _context.Appointments
                    .Where(a => a.PatientID == patient.PatientID)
                    .ToListAsync();
                _context.Appointments.RemoveRange(patientAppointments);
                _context.Patients.Remove(patient);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<UserOutputDTO>> SearchUserAsync(string Keyword)
        {
            var Users = await _context.Users
                .Where(u => u.Email.Contains(Keyword))
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserOutputDTO>>(Users);
                
        }

        public async Task<bool> deActiveUser(string NationalId)
        {

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.NationalID == NationalId);

            if(user ==null)
                return false;

            user.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateUser(string NationalId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.NationalID == NationalId);

            if (user == null)
                return false;

            user.IsActive = true;
            await _context.SaveChangesAsync();
            return true;

        }



    }
}
