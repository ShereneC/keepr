using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
    [Route("/api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _ps;
        
        public ProfilesController(ProfilesService ps)
        {
            _ps = ps;
        }
        
        [HttpGet("{id}")]

        public ActionResult<Profile> GetProfileById(string id)
        {
            try
            {
                 Profile profile = _ps.GetProfileById(id);
                 return Ok(profile);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfile(string id)
        {
        try
        {
             List<Keep> keeps = _ps.GetKeepsByProfile(id);
             return keeps;
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
        }


        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetVaultsByProfile(string id)
        {
        try
        {
             Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
             if (userInfo == null || userInfo.Id != id) {
             List<Vault> vaults = _ps.GetVaultsByProfile(id);
             return vaults;
             }
             List<Vault> vaultsOwn = _ps.GetOwnVaults(id);
             return vaultsOwn;
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
        }
    }
}