using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MedicalCenter.BLL.DTOS;
using MedicalCenter.DAL.Context;
using MedicalCenter.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalCenter.BLL.Validations
{
    public class UserValidator:AbstractValidator<UserInputDTO>
    {
        private readonly MedicalCenterDbContext _context;

        public UserValidator(MedicalCenterDbContext context)
        {
            _context = context;

            RuleFor(u => u.NationalID)
                .NotEmpty().WithMessage("National ID Required !")
                .Length(14).WithMessage("National ID must be 14 !")
                .MustAsync(BeUniqueNationalId).WithMessage("National ID Already Exist");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is Required !")
                .MustAsync(BeUniqueEmail).WithMessage("Email Already Exsit !");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is Required !")
                .MinimumLength(6).WithMessage("Min Len for password is 6 char !");



        }
        private async Task<bool> BeUniqueNationalId(string nationalId, CancellationToken cancellationToken)
        {
            return !await _context.Set<User>().AnyAsync(u => u.NationalID == nationalId, cancellationToken);
        }

        private async Task<bool> BeUniqueEmail(string Email ,CancellationToken cancellationToken)
        {
            return !await _context.Set<User>().AnyAsync(u=>u.Email == Email , cancellationToken);
        }



    }
}
