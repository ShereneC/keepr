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
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return GetById(newVault.Id);
    }

    internal Vault EditVault(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
          name = @Name,
          description = @Description,
          isPrivate = @IsPrivate
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
      return GetById(original.Id);
    }

    internal void DeleteVault(int vaultId)
    {
      string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
      _db.Execute(sql, new { vaultId });
    }
  }
}