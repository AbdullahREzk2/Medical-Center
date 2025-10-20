using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.DAL.Enums;

namespace MedicalCenter.BLL.DTOS
{
    public class UserInputDTO
    {
       
        public string NationalID { get; set; } = string.Empty;
        public GenderEnum Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RoleEnum Role { get; set; }
      

    }
}
