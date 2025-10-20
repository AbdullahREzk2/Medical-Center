using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.DAL.Enums;

namespace MedicalCenter.DAL.Entities
{
    public class User
    {
        public int UserID { get; set; }    
        public string NationalID { get; set; } = string.Empty; 
        public GenderEnum Gender { get; set; } 
        public DateTime DOB { get; set; }  
        public string Email { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty;
        public RoleEnum Role { get; set; } 
        public bool IsActive { get; set; } = true; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? LastLogin { get;set; }  


        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();


    }
}
