using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EvoteWebServer.Controllers
{
    public class Auth
    {
        public string userId { get; set; }
        public IFormFile queryFace { get; set; }
    }
}
