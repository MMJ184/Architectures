using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfinityCQRS.Backend.Contracts
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        [MaxLength(36)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Username { get; set; }

        [Required]
        [MaxLength(500)]
        public string Refreshtoken { get; set; }

        public bool Revoked { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }
    }
}
