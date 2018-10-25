using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace KinshipAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase {

        // GET api/support
        [HttpGet]
        [Route("herro")]
        public JObject Herro([FromHeader]string ClientID, [FromHeader]string ClientSecret) {
            JObject result = new JObject();
            result.Add("Herro", "Wuddap");
            return result;
        }
    }
}
