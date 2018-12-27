using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace APIHelper {
    public class Helper {
        public JObject BuildResult(int status, string message, JObject ret, string error) {
            JObject result = new JObject();
            result.Add("Status", status);
            result.Add("Message", message);

            if (ret == null) {
                result.Add("RetData", error);
            }
            else {
                foreach (var item in ret) {
                    result.Add(item.Key, item.Value);
                }
            }
            return result;
        }
        public SqlConnection OpenDBConnection(string server, string db) {
            SqlConnection dbConnection = null;
            try {
                dbConnection = new SqlConnection($"Server={server};Database={db};Trusted_Connection=true");
                dbConnection.Open();
            }
            catch (Exception e) {
                Console.WriteLine("Error connecting to db:");
                Console.WriteLine(e.Message);
            }
            return dbConnection;
        }
        public void CloseDBConnection(SqlConnection dbConnection) {
            try {
                dbConnection.Close();
            }
            catch (Exception e) {
                Console.WriteLine("Error disconnecting from db:");
                Console.WriteLine(e.Message);
            }
        }
        public static string RandomString(int length) {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider()) {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0) {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }
            return res.ToString();
        }
        public int GetUserID(string ClientID, string ClientSecret) {
            int userID = -1;
            try {
                SqlConnection dbConnection = OpenDBConnection("localhost", "kinship");

                string commandText = "SELECT user_id FROM kinship_sessions WHERE client_id = @id AND client_secret = @secret";
                using (SqlCommand command = new SqlCommand(commandText, dbConnection)) {
                    command.Parameters.Add("@id", SqlDbType.NVarChar);
                    command.Parameters["@id"].Value = ClientID;

                    command.Parameters.Add("@secret", SqlDbType.NVarChar);
                    command.Parameters["@secret"].Value = ClientSecret;

                    SqlDataReader myReader = command.ExecuteReader();
                    int resultsAmount = 0;
                    while (myReader.Read()) {
                        userID = (int)myReader["user_id"];
                        resultsAmount++;
                    }
                    if (resultsAmount > 1) {
                        userID = -1;
                        Console.WriteLine("CheckSession: THIS SHOULDN'T HAPPEN");
                    }
                }
                CloseDBConnection(dbConnection);
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return userID;
        }
        public bool CompareResourceCost(int villageID, string building) {
            bool result = true;
            JObject villageResources = GetVillageResources(villageID);
            JObject resourceCosts = GetBuildingResourceCosts(villageID, building);
            return result;
        }
        public int GetBuildingLevel(int villageID, string building) {
            int building_level = 0;
            SqlConnection dbConnection;

            dbConnection = OpenDBConnection("localhost", "kinship_world0");
            string insertText = $"SELECT {building} FROM world0_village_buildings WHERE village_id = @vid";
            using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                command.Parameters.Add("@vid", SqlDbType.Int);
                command.Parameters["@vid"].Value = villageID;

                SqlDataReader myReader = command.ExecuteReader();
                int resultsAmount = 0;
                while (myReader.Read()) {
                    building_level = (int)myReader[building];
                    resultsAmount++;
                }
                if (resultsAmount > 1) {
                    Console.WriteLine("Build: THIS SHOULDN'T HAPPEN");
                }
            }
            CloseDBConnection(dbConnection);
            return building_level;
        }
        public JObject GetVillageResources(int villageID) {
            JObject result = null;
            SqlConnection dbConnection;

            dbConnection = OpenDBConnection("localhost", "kinship_world0");
            string insertText = $"SELECT * FROM world0_village_resources WHERE village_id = @vid";
            using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {

                command.Parameters.Add("@vid", SqlDbType.Int);
                command.Parameters["@vid"].Value = villageID;

                SqlDataReader myReader = command.ExecuteReader();
                int resultsAmount = 0;
                while (myReader.Read()) {
                    result.Add("VillageID", (int)myReader["village_id"]);
                    result.Add("Wood", (int)myReader["wood"]);
                    result.Add("Stone", (int)myReader["stone"]);
                    result.Add("Iron", (int)myReader["iron"]);
                    result.Add("Food", (int)myReader["food"]);
                    result.Add("Gold", (int)myReader["gold"]);
                    //TODO add population, get from house level
                    resultsAmount++;
                }
                if (resultsAmount > 1) {
                    Console.WriteLine("Build: THIS SHOULDN'T HAPPEN");
                }
            }
            return result;
        }
        public JObject GetBuildingResourceCosts(int villageID, string building) {
            JObject result = null;
            //TODO get resource costs
            result.Add("VillageID", 1);
            result.Add("WoodCost", 1);
            result.Add("StoneCost", 1);
            result.Add("IronCost", 1);
            result.Add("FoodCost", 1);
            result.Add("GoldCost", 1);
            result.Add("HouseCost", 1);
            return result;
        }
        public int GetVillageMoral(int villageID) {
            int result = 100;
            //TODO village moral
            return result;
        }
        public JObject GetBuildingTime(string building, int buildingLevel) {
            JObject result = null;
            //TODO building time
            result.Add("Hours", 0);
            result.Add("Minutes", 1);
            result.Add("Seconds", 0);
            return result;
        }
        public int GetBuildingMaxLevel(string building) {
            int max = 0;

            if(building == "headquarters") {
                max = 40;
            }else if (building == "rally_point") {
                max = 1;
            }
            else if (building == "barracks") {
                max = 30;
            }
            else if (building == "stables") {
                max = 30;
            }
            else if (building == "workshop") {
                max = 15;
            }
            else if (building == "woodcutter") {
                max = 40;
            }
            else if (building == "quarry") {
                max = 40;
            }
            else if (building == "iron_mine") {
                max = 40;
            }
            else if (building == "farm") {
                max = 30;
            }
            else if (building == "palace") {
                max = 20;
            }
            else if (building == "warehouse") {
                max = 40;
            }
            else if (building == "barn") {
                max = 40;
            }
            else if (building == "bank") {
                max = 40;
            }
            else if (building == "market") {
                max = 30;
            }
            else if (building == "houses") {
                max = 40;
            }
            else if (building == "monastery") {
                max = 5;
            }
            else if (building == "academy") {
                max = 10;
            }
            else if (building == "university") {
                max = 20;
            }
            else if (building == "wall") {
                max = 40;
            }
            else if (building == "tower") {
                max = 10;
            }
            return max;
        }
    }
}

