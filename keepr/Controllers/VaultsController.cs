using System;
using System.Collections.Generic;
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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly VaultKeepsService _vks;
        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
        }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetVaultById(int id)
    {
        try
        {
             Vault vault = _vs.GetVaultById(id, true);
             return Ok(vault);

        }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<VaultKeepViewModel>> GetKeepsInVault(int id)
    {
      try
      {
        List<VaultKeepViewModel> vault = _vks.GetKeepsInVault(id);
        return Ok(vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
    {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             newVault.CreatorId = userInfo.Id;
             Vault vault = _vs.CreateVault(newVault);
             return Ok(vault);
        }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault editedVault)
    {
        try
        {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          editedVault.CreatorId = userInfo.Id;
          editedVault.Id = id;
          Vault vault = _vs.EditVault(editedVault);
          return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
        try
        {
           Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           _vs.DeleteVault(id, userInfo.Id);
           return Ok("The Vault Has Been Deleted!");  
        }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    }
}