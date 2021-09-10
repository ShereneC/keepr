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
    internal Keep CreateKeep(Keep newKeep)
    {
      return _repo.CreateKeep(newKeep);
    }
  }
}