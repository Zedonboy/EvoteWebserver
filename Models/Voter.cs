using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EvoteWebServer.Models
{
    public class Voter
    {
        public int Id { get; set; }
        public string userID { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string dob { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string phoneNo { get; set; }

        [MaxLength(200*200*4)]
        public byte[] profilePhoto { get; set; }
        
        [MaxLength(200*200*4)]
        public byte[] facePhoto { get; set; }
        public List<Voter_To_Candidate> voterCandidates{ get; set; }

        public List<Voters_to_ElectionType> voterElectionTypes {get; set;}
    }
}
