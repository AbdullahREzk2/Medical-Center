using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Models
{
    public class appointmentOutputModel
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; } // fk
        public string PateintName { get; set; } = string.Empty;
        public int DoctorID { get; set; } // fk
        public string DoctorName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }= string.Empty;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
