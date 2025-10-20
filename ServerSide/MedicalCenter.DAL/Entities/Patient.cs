using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.DAL.Entities
{
    public class Patient
    {
        public int PatientID { get; set; }  
        public int UserID { get; set; }     
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty; 
        public string? MedicalHistory { get; set; } 
        public string? Allergies { get; set; }


        public User User { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; }=new List<Appointment>();

    }
}
