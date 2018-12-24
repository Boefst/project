using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using APIHelper;

namespace World0.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VillageController : ControllerBase {
        Helper Helper = new Helper();

    }
}
