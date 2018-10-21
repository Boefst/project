using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AuthAPI.Controllers {
    [Route("api/[controller]")]
    public class AccountController : Controller {

        SqlConnection dbConnection = new SqlConnection("Server=localhost;Database=kinship;Trusted_Connection=true");

        private JObject BuildResult(int status, string message, JObject ret, string error) {
            JObject result = new JObject();
            result.Add("Status", status);
            result.Add("Message", message);

            if (ret == null) {
                result.Add("Data", error);
            }
            else {
                result.Add("Data", ret);
            }

            return result;
        }
        private void OpenDBConnection() {
            try {
                dbConnection.Open();
            }
            catch (Exception e) {
                Console.WriteLine("Error connecting to db:");
                Console.WriteLine(e.Message);
            }
        }
        private void CloseDBConnection() {
            try {
                dbConnection.Close();
            }
            catch (Exception e) {
                Console.WriteLine("Error disconnecting to db:");
                Console.WriteLine(e.Message);
            }
        }
        private static string RandomString(int length) {
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

        // Login user
        [HttpPost]
        [Route("login")]
        public JObject Login([FromBody]JObject userInfo) {
            JObject result = null;
            int userID = -1;
            try {
                OpenDBConnection();
                string commandText = "SELECT user_id FROM kinship_accounts WHERE account_name = @username AND account_password = @password";
                using (SqlCommand command = new SqlCommand(commandText, dbConnection)) {
                    command.Parameters.Add("@username", SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = userInfo["Username"];

                    command.Parameters.Add("@password", SqlDbType.NVarChar);
                    command.Parameters["@password"].Value = userInfo["Password"];

                    SqlDataReader myReader = command.ExecuteReader();

                    int resultsAmount = 0;
                    while (myReader.Read()) {
                        userID = (int)myReader["user_id"];
                        resultsAmount++;
                    }
                    if (resultsAmount > 1) {
                        userID = -1;
                        Console.WriteLine("Login: THIS SHOULDN'T HAPPEN");
                    }
                }
                CloseDBConnection();
                if(userID == -1){
                    result = BuildResult(401, "Unauthorized", null, "Incorrect credentials");
                }
                else {
                    OpenDBConnection();
                    string deleteText = "DELETE FROM kinship_sessions WHERE user_id = @delete_id";
                    using (SqlCommand command = new SqlCommand(deleteText, dbConnection)) {
                        command.Parameters.Add("@delete_id", SqlDbType.NVarChar);
                        command.Parameters["@delete_id"].Value = userID;

                        command.ExecuteReader();
                    }
                    CloseDBConnection();

                    Int32 rowsAffected;
                    string generatedId = RandomString(64);
                    string generatedSecret = RandomString(64);
                    OpenDBConnection();
                    string insertText = "INSERT INTO kinship_sessions (user_id, client_id, client_secret, ip_adress) VALUES(@id, @clientid, @clientsecret, @ip)";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = userID;

                        command.Parameters.Add("@clientid", SqlDbType.NVarChar);
                        command.Parameters["@clientid"].Value = generatedId;

                        command.Parameters.Add("@clientsecret", SqlDbType.NVarChar);
                        command.Parameters["@clientsecret"].Value = generatedSecret;

                        command.Parameters.Add("@ip", SqlDbType.NVarChar);
                        command.Parameters["@ip"].Value = userInfo["Ip-address"];

                        rowsAffected = command.ExecuteNonQuery();
                    }
                    CloseDBConnection();
                    if (rowsAffected == 1) {
                        JObject retObj = new JObject();
                        retObj.Add("UserID", userID);
                        retObj.Add("ClientID", generatedId);
                        retObj.Add("ClientSecret", generatedSecret);
                        result = BuildResult(200, "OK", retObj, "");
                    }
                    else {
                        result = BuildResult(500, "Internal Server Error", null, "Could not create new session");
                    }
                }
            }
            catch (Exception e) {
                result = BuildResult(500, "Internal Server Error", null, e.Message);
            }
            return result;
        }

        // Logout user
        [HttpGet]
        [Route("logout")]
        public JObject Logout([FromHeader]string ClientID, [FromHeader]string ClientSecret) {
            JObject result = null;
            int userID = -1;

            try {
                OpenDBConnection();
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
                CloseDBConnection();

                if (userID == -1) {
                    result = BuildResult(404, "Not Found", null, "Session Not Found");
                }
                else {
                    OpenDBConnection();
                    string deleteText = "DELETE FROM kinship_sessions WHERE user_id = @delete_id";
                    using (SqlCommand command = new SqlCommand(deleteText, dbConnection)) {
                        command.Parameters.Add("@delete_id", SqlDbType.NVarChar);
                        command.Parameters["@delete_id"].Value = userID;

                        command.ExecuteReader();
                    }
                    CloseDBConnection();

                    result = BuildResult(200, "OK", null, "User Logged Out");
                }
            }
            catch (Exception e) {
                result = BuildResult(500, "Internal Server Error", null, e.Message);
            }
            return result;
        }

        // Get Logged in Status
        [HttpGet]
        [Route("status")]
        public JObject CheckSession([FromHeader]string ClientID, [FromHeader]string ClientSecret) {
            JObject result = null;
            int userID = -1;
            try {
                OpenDBConnection();
                
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
                    if(resultsAmount > 1) {
                        userID = -1;
                        Console.WriteLine("CheckSession: THIS SHOULDN'T HAPPEN");
                    }
                }
                CloseDBConnection();
            }
            catch (Exception e) {
                result = BuildResult(500, "Internal Server Error", null, e.Message);
            }

            if (userID == -1) {
                result = BuildResult(404, "Not Found", null, "Session not found");
            }
            else {
                result = BuildResult(200, "OK", null, "Session Found");
            }
            return result;
        }



        /*
        // GET api/values
        [HttpGet]
        public JObject Get()
        {
            JObject temp = new JObject();
            temp.Add("get", "get");
            return temp;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JObject Get(int id)
        {
            JObject temp = new JObject();
            temp.Add("get", id);
            return temp;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]JObject value)
        {
            Console.WriteLine(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]JObject value)
        {
            Console.WriteLine(id);
            Console.WriteLine(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine(id);
        }*/
    }
}
