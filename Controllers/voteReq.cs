using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoteWebServer.Controllers
{
    public class voteReq
    {
        public int userId { get; set; }
        public int candidateId { get; set; }

        public Models.ElectionType electiontype {get; set;}
    }
}
