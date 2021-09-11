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
    internal Vault GetVaultById(int id, bool careIfPrivate = true)
    {
      Vault vault = _repo.GetById(id);
      if (vault == null || (careIfPrivate  && vault.IsPrivate == true))
      {
          throw new Exception("You are unable to view this vault");
      }
      return vault;
    }
    internal Vault CreateVault(Vault newVault)
    {
      return _repo.CreateVault(newVault);
    }


  }
}