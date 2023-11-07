using Assignment.Service.DTO.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.DTO.User
{
    public class UserResponse : BaseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<RolesResponse> Roles { get; set; }
    }
}
