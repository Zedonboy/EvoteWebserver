using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EvoteWebServer.Models;

namespace EvoteWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        private readonly EvoteWebServerContext _context;

        public ElectionsController(EvoteWebServerContext context)
        {
            _context = context;
        }

        // GET: api/Elections
        [HttpGet]
        public async Task<IEnumerable<Election>> PostElection()
        {
            if (!ModelState.IsValid)
            {
                return new List<Election>();
            }

            return await _context.Election.ToListAsync();
        }

        [HttpGet("{electionType}")]
        public async Task<IEnumerable<Candidate>> getCandidateOfElectionType([FromRoute] int electiontype)
        {
            if (!ModelState.IsValid)
            {
                return new List<Candidate>();
            }

            return await _context.Candidate.Where(c => c.electionID.Equals(electiontype)).ToListAsync();
        }

        private bool ElectionExists(int id)
        {
            return _context.Election.Any(e => e.Id == id);
        }
    }
}