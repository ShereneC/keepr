using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
      private readonly VaultsRepository _repo;
      public VaultsService(VaultsRepository repo)
      {
          _repo = repo;
      }
    internal Vault GetVaultById(int id, bool careIfPrivate)
    {
      Vault vault = _repo.GetById(id);
      if (vault == null || (careIfPrivate  && vault.IsPrivate == true))
      {
          throw new Exception("Vault does not exist OR You are unable to view this vault");
      }
      return vault;
    }
    internal Vault CreateVault(Vault newVault)
    {
      return _repo.CreateVault(newVault);
    }

    internal Vault EditVault(Vault editedVault)
    {
        // FIXME I need an if statement to determine whether I send true or false to GetById! I might have to pass in the userId!!! But then I still have to go get the Getbyid, hmmmm....
        // if (original.CreatorId )
      Vault original = GetVaultById(editedVault.Id, false);
      if (original == null)
      {
          throw new Exception("There is no vault here - bad Id");
      }
      if (original.CreatorId != editedVault.CreatorId)
      {
          throw new Exception("Not your vault to edit!");
      }
      original.Name = editedVault.Name ?? original.Name;
      original.Description = editedVault.Description ?? original.Description;
      original.IsPrivate = editedVault.IsPrivate != null ? editedVault.IsPrivate : original.IsPrivate;
      return _repo.EditVault(original);

    }
        internal void DeleteVault(int vaultId, string userId)
    {
        // also need to allow owner to getbyid and delete even if private
      Vault vaultToDelete = GetVaultById(vaultId, false);
      if (vaultToDelete.CreatorId != userId)
      {
          throw new Exception("Not your Vault to delete!");
      }
      _repo.DeleteVault(vaultId);
    }

  }
}