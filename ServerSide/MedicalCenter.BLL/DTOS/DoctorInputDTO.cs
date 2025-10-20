using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.DAL.Enums;

namespace MedicalCenter.BLL.DTOS
{
    public class DoctorInputDTO
    {

        public UserInputDTO User { get; set; }=new UserInputDTO();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public SpecializationEnum Specialization { get; set; }
        public string LicenceNumber { get; set; } = string.Empty;
        public int ShiftHours { get; set; }
        public decimal Salary { get; set; }
    }
}
