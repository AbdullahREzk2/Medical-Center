using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.DAL.Enums;

namespace MedicalCenter.DAL.Entities
{
    public class Doctor
    {
        public int DoctorID { get; set; } 
        public int UserID { get; set; }    
        public string FirstName { get; set; } = string.Empty;  
        public string LastName { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty ;
        public SpecializationEnum Specialization { get; set; } 
        public string LicenceNumber { get; set; } = string.Empty; 
        public int ShiftHours { get; set; } 
        public decimal Salary { get; set; }


        public User User { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; }=new List<Appointment>();
    }
}
