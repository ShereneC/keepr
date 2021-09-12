using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class VaultKeep
    {
        public int Id {get; set;}
        public string CreatorId { get; set; }

        [Required]
        public int VaultId {get; set;}
        public int KeepId {get; set;}
    }
}