using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Database.Entities
{
    [Table("Roles")]
    public class Roles : BaseModel
    {
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<UserToRoles> UserToRoles { get; set; }
    }
}
