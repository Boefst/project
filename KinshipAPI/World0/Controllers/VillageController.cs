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

        // village/chunk/{xcoord}/{ycoord}/{zcoord}/{chunkSize}
        [HttpGet]
        [Route("chunk/{xcoord}/{ycoord}/{zcoord}/{chunkSize}")]
        public JObject GetChunk([FromHeader]string ClientID, [FromHeader]string ClientSecret, int xcoord, int ycoord, int zcoord, int chunkSize) {
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
                    JArray retArr = new JArray();
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship_world0");
                    string insertText2 = $"SELECT * FROM world0_villages WHERE village_coord_x >= @lowerx AND village_coord_x <= @upperx AND village_coord_y >= @lowery AND village_coord_y <= @uppery AND village_coord_z >= @lowerz AND village_coord_z <= @upperz";
                    using (SqlCommand command = new SqlCommand(insertText2, dbConnection)) {
                        command.Parameters.Add("@upperx", SqlDbType.Int);
                        command.Parameters["@upperx"].Value = xcoord + chunkSize;

                        command.Parameters.Add("@lowerx", SqlDbType.Int);
                        command.Parameters["@lowerx"].Value = xcoord - chunkSize;

                        command.Parameters.Add("@uppery", SqlDbType.Int);
                        command.Parameters["@uppery"].Value = ycoord + chunkSize;

                        command.Parameters.Add("@lowery", SqlDbType.Int);
                        command.Parameters["@lowery"].Value = ycoord - chunkSize;

                        command.Parameters.Add("@upperz", SqlDbType.Int);
                        command.Parameters["@upperz"].Value = zcoord + chunkSize;

                        command.Parameters.Add("@lowerz", SqlDbType.Int);
                        command.Parameters["@lowerz"].Value = zcoord - chunkSize;

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retVillages = new JObject();
                            retVillages.Add("VillageID", (int)myReader["village_id"]);
                            retVillages.Add("OwnerID", (int)myReader["village_owner_id"]);
                            retVillages.Add("Name", (string)myReader["village_name"]);
                            retVillages.Add("XCoord", (int)myReader["village_coord_x"]);
                            retVillages.Add("YCoord", (int)myReader["village_coord_y"]);
                            retVillages.Add("ZCoord", (int)myReader["village_coord_z"]);
                            retArr.Add(retVillages);
                            resultsAmount++;
                        }
                    }
                    Helper.CloseDBConnection(dbConnection);
                    JObject retObj = new JObject();
                    retObj.Add("Villages", retArr);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

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
                    result = Helper.BuildResult(200, "OK", null, "");
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
                    string which_villages = "";
                    foreach (var item in Villages["Villages"]) {
                        if (item == Villages["Villages"].Last) {
                            which_villages += $" village_id = {item["ID"]}";
                        }
                        else {
                            which_villages += $" village_id = {item["ID"]} OR";
                        }
                    }
                    JArray retArr = new JArray();
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship_world0");
                    string insertText = $"SELECT * FROM world0_village_buildings WHERE{which_villages}";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retBuilding = new JObject();
                            retBuilding.Add("VillageID", (int)myReader["village_id"]);
                            retBuilding.Add("Headquarters", (int)myReader["headquarters"]);
                            retBuilding.Add("RallyPoint", (int)myReader["rally_point"]);
                            retBuilding.Add("Barracks", (int)myReader["barracks"]);
                            retBuilding.Add("Stables", (int)myReader["stables"]);
                            retBuilding.Add("Workshop", (int)myReader["workshop"]);
                            retBuilding.Add("Woodcutter", (int)myReader["woodcutter"]);
                            retBuilding.Add("Quarry", (int)myReader["quarry"]);
                            retBuilding.Add("IronMine", (int)myReader["iron_mine"]);
                            retBuilding.Add("Farm", (int)myReader["farm"]);
                            retBuilding.Add("Palace", (int)myReader["palace"]);
                            retBuilding.Add("Warehouse", (int)myReader["warehouse"]);
                            retBuilding.Add("Barn", (int)myReader["barn"]);
                            retBuilding.Add("Bank", (int)myReader["bank"]);
                            retBuilding.Add("Market", (int)myReader["market"]);
                            retBuilding.Add("Houses", (int)myReader["houses"]);
                            retBuilding.Add("Monastery", (int)myReader["monastery"]);
                            retBuilding.Add("Academy", (int)myReader["academy"]);
                            retBuilding.Add("University", (int)myReader["university"]);
                            retBuilding.Add("Wall", (int)myReader["wall"]);
                            retBuilding.Add("Tower", (int)myReader["tower"]);
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
        // village/build/bank/up
        [HttpPost]
        [Route("build/{villageID}/{building}/{change}")]
        public JObject Build([FromHeader]string ClientID, [FromHeader]string ClientSecret, int villageID, string building, string change) {
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
                    int building_level = Helper.GetBuildingLevel(villageID, building);

                    bool addtoqueue = false;
                    int leveltoadd = 0;
                    if (change == "up") {
                        if (building_level < Helper.GetBuildingMaxLevel(building)) {
                            if (Helper.CompareResourceCost(villageID, building)) {
                                addtoqueue = true;
                                leveltoadd = 1;
                            }
                            else{
                                result = Helper.BuildResult(400, "Bad request", null, "Not enough resources");
                            }
                        }
                        else {
                            result = Helper.BuildResult(400, "Bad request", null, "Maximum level reached");
                        }
                    }else if (change == "down") {
                        if (building_level > 0) {
                            if (true) { //TODO moral enough
                                addtoqueue = true;
                                leveltoadd = -1;
                            }
                            else {
                                result = Helper.BuildResult(400, "Bad request", null, "Not enough moral to downgrade");
                            }
                        }
                        else {
                            result = Helper.BuildResult(400, "Bad request", null, "Can't downgrade below 0");
                        }
                    }
                    else {
                        result = Helper.BuildResult(400, "Bad request", null, "Wrong parameter");
                    }

                    if (addtoqueue == true) {
                        if (change == "up") {
                            Helper.GetBuildingResourceCosts(villageID, building);
                        }
                        dbConnection = Helper.OpenDBConnection("localhost", "kinship_world0");
                        string insertText2 = "INSERT INTO world0_building_queue (village_id, building, change, time_start, time_end) VALUES (@vid, @building, @change, CURRENT_TIMESTAMP, @finished)";
                        using (SqlCommand command = new SqlCommand(insertText2, dbConnection)) {

                            command.Parameters.Add("@vid", SqlDbType.Int);
                            command.Parameters["@vid"].Value = villageID;

                            command.Parameters.Add("@building", SqlDbType.NVarChar);
                            command.Parameters["@building"].Value = building;

                            command.Parameters.Add("@change", SqlDbType.Int);
                            command.Parameters["@change"].Value = leveltoadd;

                            JObject timeToAdd = Helper.GetBuildingTime(building, building_level);
                            double hours = (double)timeToAdd["Hours"];
                            double minutes = (double)timeToAdd["Minutes"];
                            double seconds = (double)timeToAdd["Seconds"];

                            DateTime finished = DateTime.Now;
                            if (hours > 0) {
                                finished = finished.AddHours(hours);
                            }
                            if (minutes > 0) {
                                finished = finished.AddMinutes(minutes);
                            }
                            if (seconds > 0) {
                                finished = finished.AddSeconds(seconds);
                            }
                            command.Parameters.Add("@finished", SqlDbType.DateTime);
                            command.Parameters["@finished"].Value = finished;

                            SqlDataReader myReader = command.ExecuteReader();
                        }
                        Helper.CloseDBConnection(dbConnection);
                        result = Helper.BuildResult(200, "OK", null, "");
                    }
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
        // Add research to research queue
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
        // Upgrade unit
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
