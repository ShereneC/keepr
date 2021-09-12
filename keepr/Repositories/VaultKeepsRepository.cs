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
    {
      string sql = @"
      SELECT
        k.*,
        vk.id AS vaultKeepId
      FROM vaultKeeps vk
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId = @vaultId;
      ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
    }
    //     {
    //   string sql = @"
    //   SELECT
    //     a.*,
    //     vk.*,
    //     k.*
    //   FROM vaultKeeps vk
    //   JOIN accounts a ON k.creatorId = a.id,
    //   JOIN keeps k ON vk.keepId = k.id
    //   WHERE vk.vaultId = @vaultId;
    //   ";
    //   return _db.Query<Profile, VaultKeepViewModel, Vault, VaultKeepViewModel>(sql, (prof, keep, vault) => 
    //   {
    //     keep.Creator = prof;
    //     keep.Vault = vault;
    //     return keep;
    //   }, new { vaultId }, splitOn: "id").ToList<VaultKeepViewModel>();
    // }

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