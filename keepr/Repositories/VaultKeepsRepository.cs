using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {

      private readonly IDbConnection _db;
      public VaultKeepsRepository(IDbConnection db)
      {
          _db = db;
      }
    internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (vaultId, keepId, creatorId)
      VALUES
      (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    internal List<VaultKeepViewModel> GetKeepsInVault(int vaultId)
    // TODO do not understand why this one is passing "creator populated"  because it is not. Can't figure out  how to get that on there. My attempt at getting creator to populate is below - need to ask for help.
    // {
    //   string sql = @"
    //   SELECT
    //     k.*,
    //     vk.id AS vaultKeepId
    //   FROM vaultKeeps vk
    //   JOIN keeps k ON vk.keepId = k.id
    //   WHERE vk.vaultId = @vaultId;
    //   ";
    //   return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
    // }
        {
          // Harrison helped me with this... was stuck on it for hours :(
      string sql = @"
      SELECT
        vk.keepId AS vaultKeepId,
        k.*,
        a.*
      FROM vaultKeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vaultId = @vaultId;
      ";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vaultkeep, prof) => 
      {
        vaultkeep.Creator = prof;
        return vaultkeep;
      }, new { vaultId }, splitOn: "id").ToList<VaultKeepViewModel>();
    }

    internal VaultKeep GetVaultKeepById(int vkId)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @vkId";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vkId });
    }

    internal void RemoveKeepFromVault(int vkId)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @vkId LIMIT 1";
      _db.Execute(sql, new { vkId });
    }
  }
}