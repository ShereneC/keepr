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

internal Keep EditKeep(Keep updatedKeep)
    {
      Keep originalKeep = GetKeepById(updatedKeep.Id);
      // do I need a null check in here?  He doesn't have one in RestServ, but does have one in GroupMe
      if (originalKeep == null)
      {
          throw new Exception("There is no keep here- bad id");
      }
      if (originalKeep.CreatorId != updatedKeep.CreatorId)
      {
        throw new Exception("You may not edit this keep");
      }
      // NOTE these are the same kind of evaluation ('??' Null Coalesing Operator)
      originalKeep.Name = updatedKeep.Name != null ? updatedKeep.Name : originalKeep.Name;
      originalKeep.Description = updatedKeep.Description ?? originalKeep.Description;
      originalKeep.Img = updatedKeep.Img ?? originalKeep.Img;
      originalKeep.Views = updatedKeep.Views > 0 ? updatedKeep.Views : originalKeep.Views;
      originalKeep.Shares = updatedKeep.Shares > 0 ? updatedKeep.Shares : originalKeep.Shares;
      originalKeep.Keeps = updatedKeep.Keeps > 0 ? updatedKeep.Keeps : originalKeep.Keeps;
      _repo.EditKeep(originalKeep);
      return originalKeep;
    }

    internal void DeleteKeep(int keepId, string userId)
    {
      Keep keepToDelete = GetKeepById(keepId);
      if (keepToDelete.CreatorId != userId)
      {
          throw new Exception("Not your Keep to delete!");
      }
      _repo.DeleteKeep(keepId);
    }
  }
}