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
    }
}

