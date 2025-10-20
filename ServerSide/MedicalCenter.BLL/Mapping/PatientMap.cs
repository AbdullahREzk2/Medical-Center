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
    public class PatientMap:Profile
    {
        public PatientMap()
        {

            // Model -- OutputDTO 
            CreateMap<Patient, PatientOutputDTO>().ReverseMap();

            CreateMap<Patient,PatientInputDTO>().ReverseMap();
        }
    }
}
