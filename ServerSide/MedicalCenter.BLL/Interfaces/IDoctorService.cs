using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.BLL.DTOS;

namespace MedicalCenter.BLL.Interfaces
{
    public interface IDoctorService
    {

        // get All Doctors
        Task<IEnumerable<DoctorOutputDTO>> GetAllDoctors();


        // Add new Doctor
        Task<DoctorOutputDTO> AddNewDoctor(DoctorInputDTO doctorInput);
        // Delete Doctor
        Task<bool> deleteDoctorById(int Id);
        // Search
        Task<IEnumerable<DoctorOutputDTO>> SearchForDoctor(string Keyword);
    }
}
