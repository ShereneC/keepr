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
      public ProfilesService(ProfilesRepository repo, KeepsRepository krepo)
      {
          _repo = repo;
          _krepo = krepo;
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
  }
}