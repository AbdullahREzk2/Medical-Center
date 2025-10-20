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
using Microsoft.EntityFrameworkCore.Storage;

namespace MedicalCenter.BLL.Services
{
    public class DoctorService: IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly MedicalCenterDbContext _dbContext;

        public DoctorService(IMapper mapper , IUserService userService,MedicalCenterDbContext dbContext)
        {
            _mapper = mapper;
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DoctorOutputDTO>> GetAllDoctors()
        {
            var Doctors = await _dbContext.Doctors.ToListAsync();
            return _mapper.Map<IEnumerable<DoctorOutputDTO>>(Doctors);
        }

        public async Task<DoctorOutputDTO> AddNewDoctor(DoctorInputDTO doctorInput)
        {
            // Normalize email for comparison
            string normalizedEmail = doctorInput.User.Email.Trim().ToLowerInvariant();

            // Check if email already exists
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                User userEntity;

                if (existingUser != null)
                {
                    userEntity = existingUser;

                    if (userEntity.Role != RoleEnum.Doctor)
                    {
                        userEntity.Role = RoleEnum.Doctor;
                        _dbContext.Users.Update(userEntity);
                        await _dbContext.SaveChangesAsync();
                    }

                    bool alreadyDoctor = await _dbContext.Doctors
                        .AnyAsync(d => d.UserID == userEntity.UserID);

                    if (alreadyDoctor)
                        throw new Exception("❌ This user is already registered as a doctor!");
                }
                else
                {
                    // Try to create the new user
                    try
                    {
                        var createdUser = await _userService.AddNewUserAsync(doctorInput.User);

                        userEntity = await _dbContext.Users
                            .FirstOrDefaultAsync(u => u.UserID == createdUser.UserID)
                            ?? throw new Exception("❌ Failed to retrieve newly created user!");
                    }
                    catch (Exception ex)
                    {
                        // Handle unique constraint violation gracefully
                        if (ex.InnerException?.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase) == true ||
                            ex.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new Exception("❌ A user with this email already exists!");
                        }

                        throw;
                    }
                }

                // Map and save doctor
                var doctor = _mapper.Map<Doctor>(doctorInput);
                doctor.UserID = userEntity.UserID;
                doctor.User = null!;

                _dbContext.Doctors.Add(doctor);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                // Load doctor with related user data
                var savedDoctor = await _dbContext.Doctors
                    .Include(d => d.User)
                    .FirstOrDefaultAsync(d => d.DoctorID == doctor.DoctorID);

                return _mapper.Map<DoctorOutputDTO>(savedDoctor);
            }
            catch (Exception ex)
            {
                // ✅ Safe rollback: only if transaction is still active
                if (transaction.GetDbTransaction().Connection != null)
                    await transaction.RollbackAsync();

                throw new Exception($"Failed to add doctor: {ex.Message}", ex);
            }
        }

        public async Task<bool> deleteDoctorById(int Id)
        {
            var Doctor = await _dbContext.Doctors.FindAsync(Id);
            if(Doctor == null) return false;

            _dbContext.Doctors.Remove(Doctor);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DoctorOutputDTO>> SearchForDoctor(string Keyword)
        {
            var Doctors = await _dbContext.Doctors
                .Where(d=>d.FirstName == Keyword || d.LastName == Keyword)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DoctorOutputDTO>>(Doctors);
        }

        


    }
}
