using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvoteWebServer.Models
{
    public class Voter_To_Candidate
    {
        public int Id { get; set; }
        public int voterId { get; set; }

        [ForeignKey("voterId")]
        public Voter voter { get; set; }
        public int canditateId { get; set; }
        [ForeignKey("candidateId")]
        public Candidate candidate { get; set; }

        public ElectionType electiontype {get; set;}
    }
}
