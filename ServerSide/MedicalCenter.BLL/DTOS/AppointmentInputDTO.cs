using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.DAL.Enums;

namespace MedicalCenter.BLL.DTOS
{
    public class AppointmentInputDTO
    {

        public int PatientID { get; set; } // fk
        public int DoctorID { get; set; } // fk
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatusEnum Status { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
