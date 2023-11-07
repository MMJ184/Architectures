using Microsoft.AspNetCore.Identity;

namespace InfinityCQRS.Backend.Contracts
{
    public partial class ApplicationRole : IdentityRole<string>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
