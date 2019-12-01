using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvoteWebServer.Models;
using System.IO;
using Dotnetbiometric;

namespace EvoteWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EvoteWebServerContext _context;

        public AuthController(EvoteWebServerContext context)
        {
            _context = context;
        }

        // POST: api/Auth
        [HttpPost]
        public async Task<IActionResult> PostVoter([FromForm] Auth auth)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (auth == null) return BadRequest();
            List<Voter> voterlist;
            try
            {
                voterlist = await _context.Voter
                .Where(v => v.userID.Contains(auth.userId))
                .ToListAsync();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new {message = "Database is not available"});
            }
            ;
            if (voterlist.Count == 0) return NoContent();
            var authedVoter = voterlist[0];
            byte[] queryImgBytes, targetImgBytes;
            using(var ms = new MemoryStream())
            {
                await auth.queryFace.CopyToAsync(ms);
                queryImgBytes = ms.ToArray();
                targetImgBytes = authedVoter.facePhoto;
            }
            Biometric.test();
            float descionscore = 0.000F;
            descionscore = Biometric.second_compareImagesMat(targetImgBytes.Length, targetImgBytes, queryImgBytes.Length, queryImgBytes);
            if(descionscore >= 0.6)
            {
                return Ok(authedVoter);
            } else
            {
                return Unauthorized();
            }
        }

        private bool VoterExists(int id)
        {
            return _context.Voter.Any(e => e.Id == id);
        }
    }
}