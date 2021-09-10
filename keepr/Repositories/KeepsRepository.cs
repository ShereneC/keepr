using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    internal List<Keep> GetAllKeeps()
    // This postman test is passing, I guess it didn't need to populate the creator.  I'm leaving it as is for now.
    {
      string sql = "SELECT * FROM keeps;";
      return _db.Query<Keep>(sql).ToList();
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @" 
            SELECT 
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = @id";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
          keep.Creator = prof;
          return keep;
      }, new { id }, splitOn: "id").FirstOrDefault();
      }
    

    internal Keep CreateKeep(Keep newKeep)
    // passing Postman tests
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, shares, keeps, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      // I think this attaches the id to it and sends the id back (attached to the newKeep)
      // REVIEW 1 Failed this test because I'm not populating the object creator.  I think if I do a getbyId here it will handle that, so I'll move on to Get By ID
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return GetKeepById(newKeep.Id);
    }

  }
}