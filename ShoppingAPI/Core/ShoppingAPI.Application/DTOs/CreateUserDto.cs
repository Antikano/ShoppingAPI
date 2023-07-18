using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Domain.DTOs
{
    public class CreateUserDto
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
