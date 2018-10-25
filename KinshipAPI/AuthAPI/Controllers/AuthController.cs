using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;
using APIHelper;

namespace AuthAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        Helper Helper = new Helper();
        // Login user
        [HttpPost]
        [Route("login")]
        public JObject Login([FromBody]JObject userInfo) {
            JObject result = null;
            int userID = -1;
            string status = "";
            SqlConnection dbConnection;
            try {
                int resultsAmount = 0;
                string generatedId = Helper.RandomString(64);
                string generatedSecret = Helper.RandomString(64);
                dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                string insertText = "EXEC procedure_login @username = @user, @password = @pass, @client_id = @clientid, @client_secret = @clientsecret, @ip_address = @ip";
                using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                    command.Parameters.Add("@user", SqlDbType.NVarChar);
                    command.Parameters["@user"].Value = userInfo["Username"];

                    command.Parameters.Add("@pass", SqlDbType.NVarChar);
                    command.Parameters["@pass"].Value = userInfo["Password"];

                    command.Parameters.Add("@clientid", SqlDbType.NVarChar);
                    command.Parameters["@clientid"].Value = generatedId;

                    command.Parameters.Add("@clientsecret", SqlDbType.NVarChar);
                    command.Parameters["@clientsecret"].Value = generatedSecret;

                    command.Parameters.Add("@ip", SqlDbType.NVarChar);
                    command.Parameters["@ip"].Value = userInfo["Ip-address"];

                    SqlDataReader myReader = command.ExecuteReader();

                    while (myReader.Read()) {
                        if ((int)myReader["user_id"] != -1) {
                            userID = (int)myReader["user_id"];
                        }
                        status = (string)myReader["status"];
                        resultsAmount++;
                    }
                    if (resultsAmount > 1) {
                        userID = -1;
                        Console.WriteLine("Login: THIS SHOULDN'T HAPPEN");
                    }
                }
                Helper.CloseDBConnection(dbConnection);

                if (userID == -1) {
                    result = Helper.BuildResult(401, "Unauthorized", null, status);
                }
                else {
                    JObject retObj = new JObject();
                    retObj.Add("UserID", userID);
                    retObj.Add("ClientID", generatedId);
                    retObj.Add("ClientSecret", generatedSecret);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
            }
            catch (Exception e) {
                result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
            }
            return result;
        }

        // Logout user
        [HttpGet]
        [Route("logout")]
        public JObject Logout([FromHeader]string ClientID, [FromHeader]string ClientSecret) {
            JObject result = null;
            int userID = -1;
            SqlConnection dbConnection;

            try {
                userID = Helper.GetUserID(ClientID, ClientSecret);

                if (userID == -1) {
                    result = Helper.BuildResult(404, "Not Found", null, "Session Not Found");
                }
                else {
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string deleteText = "DELETE FROM kinship_sessions WHERE user_id = @delete_id";
                    using (SqlCommand command = new SqlCommand(deleteText, dbConnection)) {
                        command.Parameters.Add("@delete_id", SqlDbType.NVarChar);
                        command.Parameters["@delete_id"].Value = userID;

                        command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);

                    result = Helper.BuildResult(200, "OK", null, "User Logged Out");
                }
            }
            catch (Exception e) {
                result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
            }
            return result;
        }

        // Get Logged in Status
        [HttpGet]
        [Route("status")]
        public JObject CheckSession([FromHeader]string ClientID, [FromHeader]string ClientSecret) {
            JObject result = null;
            int userID = Helper.GetUserID(ClientID, ClientSecret);

            if (userID == -1) {
                result = Helper.BuildResult(404, "Not Found", null, "Session not found");
            }
            else {
                result = Helper.BuildResult(200, "OK", null, "Session Found");
            }
            return result;
        }
    }
}
