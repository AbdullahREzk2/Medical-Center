using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Models
{
    public class DoctorInputModel
    {
        public UserInputModel User { get; set; } = new UserInputModel();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Specialization { get; set; }= string.Empty;  
        public string LicenceNumber { get; set; } = string.Empty;
        public int ShiftHours { get; set; }
        public decimal Salary { get; set; }
    }
}
