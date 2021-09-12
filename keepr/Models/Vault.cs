using System;
using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class Vault
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsPrivate { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
//NOTE do I need this view model???
    //   public class VaultKeepViewModel : Vault
    // {
    //   public int VaultKeepId { get; set; }
    // }
}