using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvoteWebServer.Models;

namespace EvoteWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotersController : ControllerBase
    {
        private readonly EvoteWebServerContext _context;

        public VotersController(EvoteWebServerContext context)
        {
            _context = context;
        }

        // POST: api/Voters
        [HttpPost("vote")]
        public async Task<IActionResult> PostVoter([FromBody] voteReq voteReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Voter voter;
            try
            {
                voter = await _context.Voter
                                    .Include(v => v.voterElectionTypes)
                                    .Where(b => b.Id.Equals(voteReq.userId))
                                    .SingleAsync();
            }
            catch (Exception)
            {
                return UnprocessableEntity("userId not found");
            }
            // if he has voted for this election
            // FInd if he has voted for this candidate
            var votedCandidate = voter.voterElectionTypes.Find(c => c.electiontype == voteReq.electiontype);
            if (votedCandidate != null) return Conflict(new { message = "You have voted for this election" });
            var voterCandidate = new Voter_To_Candidate{
                voterId = voteReq.userId,
                canditateId = voteReq.candidateId
            };
            var voterElectiontype = new Voters_to_ElectionType
            {
                voterId = voteReq.userId,
                electiontype = voteReq.electiontype
            };

            try
            {
                await _context.votersELectionType.AddAsync(voterElectiontype);
                await _context.votersCandidate.AddAsync(voterCandidate);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new {message = "Database not available, try again"});
            }
            return Ok("voted successfully");
        }

        [HttpPost("create")]
        public async Task<IActionResult> createVoter([FromForm] voterCreate vc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var ms = new MemoryStream())
            {
                byte[] facePhoto;
                byte[] profilePhoto;
                await vc.profilePhoto.CopyToAsync(ms);
                profilePhoto = ms.ToArray();
                ms.SetLength(0);
                await vc.facePhoto.CopyToAsync(ms);
                facePhoto = ms.ToArray();
                var voter = new Voter
                {
                    middlename = vc.middlename,
                    firstname = vc.firstname,
                    dob = vc.dob,
                    userID = vc.userID,
                    phoneNo = vc.phoneNo,
                    gender = vc.gender,
                    profilePhoto = profilePhoto,
                    facePhoto = facePhoto
                };
                try
                {
                    await _context.Voter.AddAsync(voter);
                    await _context.SaveChangesAsync();
                }
                catch (System.Exception)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = "DataBase is not available" });
                }
                return Ok(new { message = "Voter creation was succesfull" });
            }
        }
        private bool VoterExists(int id)
        {
            return _context.Voter.Any(e => e.Id == id);
        }
    }
}