using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Database.Entities
{
    public class UserToRoles : BaseModel
    {
        [Required]
        public int UsersId { get; set; }
        [ForeignKey(nameof(UsersId))]
        public virtual Users Users { get; set; }

        [Required]
        public int RolesId { get; set; }
        [ForeignKey(nameof(RolesId))]
        public virtual Roles Roles { get; set; }
    }
}
