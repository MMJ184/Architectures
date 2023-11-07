using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Database.Entities
{
    public class BaseModel
    {

        [Key]
        public int Id { get; set; }
        public bool IsDisabled { get; set; } = false;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public void Update(int modifiedBy)
        {
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = DateTime.UtcNow;
        }
        public void Add(int createdBy)
        {
            this.CreatedBy = createdBy;
            this.CreatedDate = DateTime.UtcNow;
            IsDisabled = false;
        }
        public void Delete(int modifiedBy)
        {
            Update(modifiedBy);
            IsDisabled = true;
        }
    }
}
