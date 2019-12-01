using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EvoteWebServer.Controllers
{
    public class voterCreate
    {
        public string userID {get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string dob { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string phoneNo { get; set; }
        public IFormFile profilePhoto { get; set; }
        public IFormFile facePhoto { get; set; }
    }
}
