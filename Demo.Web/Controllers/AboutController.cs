using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Controllers
{
    [Route("[controller]")]//[Route("[controller]/[action]")]
    public class AboutController
    {
        [Route("")]
        public string Me()
        {
            return "Liu";
        }
        [Route("company")]
        public string Company()
        {
            return "No Company";
        }
    }
}
