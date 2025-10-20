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
    public class appointmentMap:Profile
    {
        public appointmentMap()
        {
            CreateMap<Appointment,AppointmentInputDTO>().ReverseMap();

            CreateMap<Appointment, AppointmentOutputDTO>()
            .ForMember(dest => dest.DoctorName,
                opt => opt.MapFrom(src =>
                    src.Doctor != null
                    ? src.Doctor.FirstName + " " + src.Doctor.LastName
                    : string.Empty))
            .ForMember(dest => dest.PateintName,
                 opt => opt.MapFrom(src =>
                     src.Patient != null
                    ? src.Patient.FirstName + " " + src.Patient.LastName
                    : string.Empty));
        }
    }
}
