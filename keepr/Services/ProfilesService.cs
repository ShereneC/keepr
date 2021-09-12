using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {
      private readonly ProfilesRepository _repo;
      private readonly KeepsRepository _krepo;
      private readonly VaultsRepository _vrepo;
      public ProfilesService(ProfilesRepository repo, KeepsRepository krepo, VaultsRepository vrepo)
      {
          _repo = repo;
          _krepo = krepo;
          _vrepo = vrepo;
      }
    internal Profile GetProfileById(string id)
    {
      Profile profile = _repo.GetProfileById(id);
      if (profile == null)
      {
          throw new Exception("Invalid Profile Id");
      }
      return profile;
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
      List<Keep> keeps = _krepo.GetKeepsByProfile(profileId);
      return keeps;
    }

    internal List<Vault> GetVaultsByProfile(string profileId)
    {
      List<Vault> vaults = _vrepo.GetVaultsByProfile(profileId);
      return vaults;
    }

    internal List<Vault> GetOwnVaults(string profileId)
    {

        List<Vault> vaults = _vrepo.GetOwnVaults(profileId);

    return vaults;
    }
  }
}