using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.BLL.DTOS;

namespace MedicalCenter.BLL.Interfaces
{
    public interface IPatientService
    {

        // Get All Pateints 
         Task<IEnumerable<PatientOutputDTO>> GetAllPateints();


        // Add New Pateint 
        Task<PatientOutputDTO> AddNewPateint(PatientInputDTO patientInput);

        // Delete Patient with patient ID
        Task<bool> deletePateintwithId(int Id);

        // Search for pateint

        Task<IEnumerable<PatientOutputDTO>> SearchPateints(string Keyword);
    }
}
