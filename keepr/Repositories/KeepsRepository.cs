using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
      private readonly IDbConnection _db;
      public KeepsRepository(IDbConnection db)
      {
          _db = db;
      }
    internal Keep CreateKeep(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, shares, keeps, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      // I think this attaches the id to it and sends the id back (attached to the newKeep)
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }


  }
}