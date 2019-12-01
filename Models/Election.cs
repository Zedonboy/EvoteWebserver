using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoteWebServer.Models
{
    public class Election
    {
        public int Id { get; set; }
        public ElectionType electionType { get; set; }
        public string name_of_election { get; set; }
        public List<Candidate> candidates { get; set; }
    }

    public enum ElectionType
    {
        GOVERNOR,
        PRESIDENTIAL,
        SENATORIAL
    }
}
