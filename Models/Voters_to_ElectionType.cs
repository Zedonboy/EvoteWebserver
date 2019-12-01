using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvoteWebServer.Models
{
    public class Voters_to_ElectionType
    {
        public int Id { get; set; }
        public int voterId { get; set; }

        [ForeignKey("voterId")]
        public Voter voter { get; set; }
        public ElectionType electiontype {get; set;}
    }
}
