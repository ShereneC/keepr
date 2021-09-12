using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsRepository _vaultsRepo;
        public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vaultsRepo)
        {
            _repo = repo;
            _vaultsRepo = vaultsRepo;
        }
    internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, string userId)
    {
      Vault foundVault = _vaultsRepo.GetById(newVaultKeep.VaultId);
      if (foundVault.CreatorId != userId)
      {
          throw new Exception("You may not add keeps to a vault that is not your own.");
      }
      return _repo.CreateVaultKeep(newVaultKeep);
    }

    internal List<VaultKeepViewModel> GetKeepsInVault(int vaultId)
    {
      Vault foundVault = _vaultsRepo.GetById(vaultId);
      if (foundVault.IsPrivate == true)
      {
          throw new Exception("Sorry, this is a private vault.");
      }
        return _repo.GetKeepsInVault(vaultId);
    }
  }
}