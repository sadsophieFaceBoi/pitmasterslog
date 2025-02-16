using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Types
{
    public class User
    {
        public string Id { get; set; }= string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AuthProvider { get; set; } = string.Empty;
        public string AuthId { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime SignupDate { get; set; }
        public DateTime LastLogin { get; set; }
        public List<LoginDetails> LoginDetails { get; set; } = new List<LoginDetails>();
        public bool IsEmailVerified { get; set; }



    }
    
   
    public class LoginDetails
    {
        public string IPAdress { get; set; } 
        public string Device { get; set; } 
        public string Browser { get; set; } 
        public DateTime LoginTime { get; set; }
        public string Location { get; set; } 
        public bool IsActive { get; set; }

    }
}
