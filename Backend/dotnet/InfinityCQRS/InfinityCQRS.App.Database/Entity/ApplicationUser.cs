using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InfinityCQRS.Backend.Contracts
{
    public class ApplicationUser : IdentityUser, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        [Required]
        public string Language { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Soft Deletes Entity
        /// </summary>
        public void Delete() => OnDelete();

        /// <summary>
        /// Logic for deleting entity (Ex: Cascade deletion)
        /// </summary>
        protected virtual void OnDelete()
            => IsDeleted = true;
    }
}
