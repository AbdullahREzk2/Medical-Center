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
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly MedicalCenterDbContext _context;

        public PatientService(IMapper mapper, IUserService userService, MedicalCenterDbContext context)
        {
            _mapper = mapper;
            _userService = userService;
            _context = context;
        }

        public async Task<IEnumerable<PatientOutputDTO>> GetAllPateints()
        {
            var patients = await _context.Patients
                .Include(p => p.User)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PatientOutputDTO>>(patients);
        }

        public async Task<PatientOutputDTO> AddNewPateint(PatientInputDTO patientInput)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var normalizedEmail = patientInput.User.Email.Trim().ToLowerInvariant();

                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

                User? userEntity;

                if (existingUser != null)
                {
                    userEntity = existingUser;

                    if (userEntity.Role != RoleEnum.Patient)
                    {
                        userEntity.Role = RoleEnum.Patient;
                        _context.Users.Update(userEntity);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    try
                    {
                        var createdUser = await _userService.AddNewUserAsync(patientInput.User);
                        userEntity = await _context.Users.FindAsync(createdUser.UserID);

                        if (userEntity == null)
                        {
                            throw new Exception("Failed to retrieve newly created user.");
                        }
                    }
                    catch (DbUpdateException ex) when (ex.InnerException?.Message?.Contains("IX_Users_Email") == true)
                    {
                        userEntity = await _context.Users
                            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

                        if (userEntity == null)
                        {
                            throw new Exception($"Email already exists but user not found: {normalizedEmail}", ex);
                        }
                    }
                }

                var existingPatient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.UserID == userEntity.UserID);

                if (existingPatient != null)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("This user is already registered as a patient.");
                }

                var patient = _mapper.Map<Patient>(patientInput);
                patient.UserID = userEntity.UserID;
                patient.User = null!; 

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var patientWithUser = await _context.Patients
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.PatientID == patient.PatientID);

                return _mapper.Map<PatientOutputDTO>(patientWithUser);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Database error while adding patient: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
            catch (Exception )
            {
                await transaction.RollbackAsync();
                throw ;
            }
        }

        public async Task<bool> deletePateintwithId(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.PatientID == id);

            if (patient == null) return false;

            _context.Appointments.RemoveRange(patient.Appointments);

            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PatientOutputDTO>> SearchPateints(string Keyword)
        {
            var Pateints = await _context.Patients
                .Where(p => p.FirstName.Contains(Keyword) || p.LastName.Contains(Keyword))
                .ToListAsync();

            return _mapper.Map<IEnumerable<PatientOutputDTO>>(Pateints);
        }


    }
}
