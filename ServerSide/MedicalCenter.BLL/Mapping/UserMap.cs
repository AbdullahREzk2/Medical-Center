using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedicalCenter.BLL.DTOS;
using MedicalCenter.DAL.Entities;

namespace MedicalCenter.BLL.Mapping
{
    public class UserMap:Profile
    {
        public UserMap()
        {
            // Map model -->output DTO
            CreateMap<User, UserOutputDTO>().ReverseMap();


            // map input DTO --> Model 
            CreateMap<UserInputDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));
        }
    }
}
