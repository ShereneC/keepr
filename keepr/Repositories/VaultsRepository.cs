using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
      private readonly IDbConnection _db;
      public VaultsRepository(IDbConnection db)
      {
          _db = db;
      }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT
      a.*,
      v.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @id;
      ";
      return _db.Query<Profile, Vault, Vault>(sql, (prof, vault) =>
      {
          vault.Creator = prof;
          return vault;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal Vault CreateVault(Vault newVault)
    // passing postman except for data.creator, soI think I need to populate, will do that by doing getbyid after that is made.
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      // I think this attaches the id to it and sends the id back (attached to the newKeep)
      // REVIEW 1 Failed this test because I'm not populating the object creator.  I think if I do a getbyId here it will handle that, so I'll move on to Get By ID
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return GetById(newVault.Id);
    }

  }
}