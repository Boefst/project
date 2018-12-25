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

        // village/rename/herro/2
        [HttpPost]
        [Route("rename/{name}/{village_id}")]
        public JObject Rename([FromHeader]string ClientID, [FromHeader]string ClientSecret, string name, int village_id) {
            JObject result = null;
            int userID = -1;
            SqlConnection dbConnection;
            try {
                userID = Helper.GetUserID(ClientID, ClientSecret);
            }
            catch (Exception e) {
                result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
            }
            if (userID == -1) {
                result = Helper.BuildResult(404, "Not Found", null, "Session not found");
            }
            else {
                try {
                    JObject retObj = new JObject();
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship_world0");
                    string insertText = $"UPDATE world0_villages SET village_name = @new WHERE village_id = @vid";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@new", SqlDbType.NVarChar);
                        command.Parameters["@new"].Value = name;

                        command.Parameters.Add("@vid", SqlDbType.Int);
                        command.Parameters["@vid"].Value = village_id;

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
        // village/buildings
        [HttpGet]
        [Route("buildings")]
        public JObject Buildings([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Villages) {
            JObject result = null;
            int userID = -1;
            SqlConnection dbConnection;
            try {
                userID = Helper.GetUserID(ClientID, ClientSecret);
            }
            catch (Exception e) {
                result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
            }
            if (userID == -1) {
                result = Helper.BuildResult(404, "Not Found", null, "Session not found");
            }
            else {
                try {
                    int count = 0;
                    string which_villages = "";
                    foreach (var item in Villages) {
                        count++;
                        Console.WriteLine("Count: " + count + " JObjectcount: " + Villages.Count);
                        if (count < Villages.Count) {
                            which_villages += $"village_id = {Villages["VillageID"]} AND ";
                        }
                        else {
                            which_villages += $"village_id = {Villages["VillageID"]}";
                        }
                    }
                    JArray retArr = new JArray();
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship_world0");
                    string insertText = $"SELECT * FROM world0_village_buildings WHERE {which_villages}";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retBuilding = new JObject();
                            retBuilding.Add("VillageID", (int)myReader["village_id"]);
                            retBuilding.Add("Headquarters", (string)myReader["headquarters"]);
                            retBuilding.Add("Barracks", (string)myReader["barracks"]);
                            retArr.Add(retBuilding);
                            resultsAmount++;
                        }
                    }
                    Helper.CloseDBConnection(dbConnection);
                    JObject retObj = new JObject();
                    retObj.Add("Buildings", retArr);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
        // Add building to building queue
        [HttpPost]
        [Route("build/{building}/{change}")]
        public JObject Build([FromHeader]string ClientID, [FromHeader]string ClientSecret, string building, string change) {
            JObject result = null;
            return result;
        }
        // Add building to building queue
        [HttpPost]
        [Route("research/{research}/{change}")]
        public JObject Research([FromHeader]string ClientID, [FromHeader]string ClientSecret, string research, string change) {
            JObject result = null;
            return result;
        }
        // Add troops to troops queue
        [HttpPost]
        [Route("recruit/{unit}/{change}")]
        public JObject Recruit([FromHeader]string ClientID, [FromHeader]string ClientSecret, string unit, string change) {
            JObject result = null;
            return result;
        }
        // Add troops to troops queue
        [HttpPost]
        [Route("upgrade/{unit}/{change}")]
        public JObject Upgrade([FromHeader]string ClientID, [FromHeader]string ClientSecret, string unit, string change) {
            JObject result = null;
            return result;
        }
        // Send troops
        [HttpPost]
        [Route("command")]
        public JObject Command([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject CommandInfo) {
            JObject result = null;
            return result;
        }
    }
}
