using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class Keep
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public int Keeps { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }

    public class VaultKeepViewModel : Keep
    {
      public int VaultKeepId { get; set; }
      
      // REVIEW I added Vault (below) to try and get the populate to work on VaultKeepsRepo GetKeepsInVault  - do I need this?
      public Vault Vault {get; set;}
    }
}