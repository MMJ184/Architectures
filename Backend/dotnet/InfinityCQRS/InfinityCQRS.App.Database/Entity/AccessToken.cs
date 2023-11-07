using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfinityCQRS.Backend.Contracts
{
    [Table("AccessToken")]
    public class AccessToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public bool Revoked { get; set; }

        public DateTime LastModifiedOn { get; set; }
    }
}
