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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        public VaultsController(VaultsService vs)
        {
            _vs = vs;
        }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetVaultById(int id)
    {
        try
        {
             Vault vault = _vs.GetVaultById(id);
             return Ok(vault);

        }
      catch (Exception err)
      {
        return BadRequest(err.Message);
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


    }
}