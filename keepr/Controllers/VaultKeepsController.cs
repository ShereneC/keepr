using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;
        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }


    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
    {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             newVaultKeep.CreatorId = userInfo.Id;
             VaultKeep vaultkeep = _vks.CreateVaultKeep(newVaultKeep, userInfo.Id);
             return Ok(vaultkeep);
        }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
           Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           _vks.RemoveKeepFromVault(id, userInfo.Id);
           return Ok("The keep has been removed");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    }
}