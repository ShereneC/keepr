using System;
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
    }
}