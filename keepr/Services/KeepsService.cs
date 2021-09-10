using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
      private readonly KeepsRepository _repo;
      public KeepsService(KeepsRepository repo)
      {
          _repo = repo;
      }
    internal List<Keep> GetAllKeeps()
    {
      return _repo.GetAllKeeps();
    }
    internal Keep GetKeepById(int id)
    {
      Keep foundKeep = _repo.GetKeepById(id);
      if (foundKeep == null)
      {
          throw new Exception("Invalid Keep Id");
      }
      return foundKeep;
    }
    internal Keep CreateKeep(Keep newKeep)
    {
      return _repo.CreateKeep(newKeep);
    }

  }
}