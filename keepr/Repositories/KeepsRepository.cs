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
    public List<Keep> GetAllKeeps()
    // This postman test is passing, I guess it didn't need to populate the creator.  I'm leaving it as is for now.
    {
      string sql = @"
      SELECT
      a.*,
      k.* 
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId;";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, splitOn: "id").ToList();
    }

    public Keep GetKeepById(int id)
    {
      string sql = @"
            UPDATE keeps k
            SET k.views = k.views+1
            WHERE k.id = @id; 
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


    public Keep CreateKeep(Keep newKeep)
    // passing Postman tests
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, shares, keeps, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return GetKeepById(newKeep.Id);
    }

    internal List<Keep> GetKeepsByProfile(string profileId)
    {
      string sql = @"
      SELECT 
        a.*,
        k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @profileId;";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { profileId }, splitOn: "id").ToList<Keep>();
    }

    public Keep EditKeep(Keep updatedKeep)
    //passing postman tests
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps
      WHERE id = @Id
      ;";
      _db.Execute(sql, updatedKeep);
      return GetKeepById(updatedKeep.Id);
    }

    internal void DeleteKeep(int id)
    // passing postman test
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}