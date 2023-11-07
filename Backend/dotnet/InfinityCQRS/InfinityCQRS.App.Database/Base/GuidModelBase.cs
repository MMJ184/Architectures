using System.ComponentModel.DataAnnotations;

namespace InfinityCQRS.Backend.Contracts
{
    public abstract class GuidModelBase
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        protected GuidModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
