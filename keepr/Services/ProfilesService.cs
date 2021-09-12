using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class ProfilesService
  {
      private readonly ProfilesRepository _repo;
      public ProfilesService(ProfilesRepository repo)
      {
          _repo = repo;
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
  }
}