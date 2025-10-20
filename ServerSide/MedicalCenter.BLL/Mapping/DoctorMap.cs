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
    public class DoctorMap:Profile
    {
        public DoctorMap()
        {
            CreateMap<Doctor, DoctorOutputDTO>().ReverseMap();

            CreateMap<Doctor, DoctorInputDTO>().ReverseMap();
        }
    }
}
