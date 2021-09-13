using System;
using System.Runtime.InteropServices;
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
    internal Vault GetVaultById(int id, [Optional] string userId)
    {
      Vault vault = _repo.GetById(id);
      if (((userId != null && vault.CreatorId != userId) || userId == null) && vault.IsPrivate == false)
      // If they are not the creator or not logged in AND it is not a private vault, then return it.
      {
        return vault;
      }
      if (((userId != null && vault.CreatorId != userId) || userId == null) && vault.IsPrivate == true)
      //If they are not the creator or not logged in AND it is a private vault, throw the error
      // NOTE I am no longer null checking.
      {
        throw new Exception("You are unable to view this vault");
      }
      //if they get this far, they are the owner, so they can get the vault.
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
      Vault original = GetVaultById(editedVault.Id);
      //took out the false variable here (above)
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
      Vault vaultToDelete = GetVaultById(vaultId);
      // took out the false variable (above)
      if (vaultToDelete.CreatorId != userId)
      {
          throw new Exception("Not your Vault to delete!");
      }
      _repo.DeleteVault(vaultId);
    }

  }
}