using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvoteWebServer.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string politicalParty { get; set; }
        public int electionID { get; set; }

        [ForeignKey("electionID")]
        public Election election { get; set; }

        public List<Voter_To_Candidate> voterCandidates{ get; set; }

        public ElectionType electiontype{get; set;}
    }
}
