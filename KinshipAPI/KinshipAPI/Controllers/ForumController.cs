using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using APIHelper;

namespace KinshipAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase {
        Helper Helper = new Helper();

        // POST api/forum/thread/create
        [HttpPost]
        [Route("thread/create")]
        public JObject CreateThread([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Thread) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "EXEC procedure_create_forum_thread @category = @cat, @title = @tit, @creator = @crea, @message = @mess, @isAdmin = 0";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@cat", SqlDbType.Int);
                        command.Parameters["@cat"].Value = Thread["Category"];

                        command.Parameters.Add("@tit", SqlDbType.NVarChar);
                        command.Parameters["@tit"].Value = Thread["Title"];

                        command.Parameters.Add("@crea", SqlDbType.Int);
                        command.Parameters["@crea"].Value = userID;

                        command.Parameters.Add("@mess", SqlDbType.NVarChar);
                        command.Parameters["@mess"].Value = Thread["Message"];

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);

                    result = Helper.BuildResult(200, "OK", null, "Thread Created");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // POST api/forum/thread/edit
        [HttpPost]
        [Route("thread/edit")]
        public JObject EditThread([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Thread) {
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
                    string parameters = "";
                    int par = 0;
                    if (Thread["Category"] != null) {
                        parameters += "category_id = " + Thread["Category"];
                        par++;
                    }
                    if (Thread["Title"] != null) {
                        if(par > 0) {
                            parameters += ", ";
                        }
                        parameters += "title = N'" + Thread["Title"] + "'";
                        par++;
                    }
                    if (parameters != "") {
                        dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                        string insertText = $"UPDATE kinship_forum_threads SET {parameters} WHERE thread_id = @tid";
                        using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                            command.Parameters.Add("@tid", SqlDbType.Int);
                            command.Parameters["@tid"].Value = Thread["ID"];
                            
                            SqlDataReader myReader = command.ExecuteReader();
                        }
                        Helper.CloseDBConnection(dbConnection);

                        result = Helper.BuildResult(200, "OK", null, "Thread Edited");
                    }
                    else {
                        result = Helper.BuildResult(404, "Not Found", null, "No parameters");
                    }
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // POST api/forum/thread/delete
        [HttpPost]
        [Route("thread/delete")]
        public JObject DeleteThread([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Thread) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "UPDATE kinship_forum_threads SET deleted = @delet, reason = @reas WHERE thread_id = @tid";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@tid", SqlDbType.NVarChar);
                        command.Parameters["@tid"].Value = Thread["ID"];

                        command.Parameters.Add("@reas", SqlDbType.NVarChar);
                        command.Parameters["@reas"].Value = Thread["Reason"];

                        command.Parameters.Add("@delet", SqlDbType.Bit);
                        command.Parameters["@delet"].Value = 1;

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);

                    result = Helper.BuildResult(200, "OK", null, "Thread Deleted");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // GET api/forum/threads/{category}
        [HttpGet]
        [Route("threads/{category}")]
        public JObject GetThreads([FromHeader]string ClientID, [FromHeader]string ClientSecret, int category) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "SELECT thread_id, title, Acc.account_name, create_time FROM kinship_forum_threads AS Thread INNER JOIN kinship_accounts AS Acc ON Thread.creator_id = Acc.user_id WHERE category_id = @cat AND deleted = 0";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@cat", SqlDbType.Int);
                        command.Parameters["@cat"].Value = category;

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retThread = new JObject();
                            retThread.Add("ThreadID", (int)myReader["thread_id"]);
                            retThread.Add("Title", (string)myReader["title"]);
                            retThread.Add("Name", (string)myReader["account_name"]);
                            retThread.Add("Created", (DateTime)myReader["create_time"]);
                            retArr.Add(retThread);
                            resultsAmount++;
                        }
                    }
                    Helper.CloseDBConnection(dbConnection);
                    JObject retObj = new JObject();
                    retObj.Add("Threads", retArr);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // POST api/forum/post/create
        [HttpPost]
        [Route("post/create")]
        public JObject CreatePost([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Post) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "INSERT INTO kinship_forum_messages (thread_id, message_text, sender_id, is_admin, sent_time, deleted) VALUES (@tid, @mess, @userid, 0, CURRENT_TIMESTAMP, 0)";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@tid", SqlDbType.Int);
                        command.Parameters["@tid"].Value = Post["ThreadID"];

                        command.Parameters.Add("@mess", SqlDbType.NVarChar);
                        command.Parameters["@mess"].Value = Post["Message"];

                        command.Parameters.Add("@userid", SqlDbType.Int);
                        command.Parameters["@userid"].Value = userID;

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);

                    result = Helper.BuildResult(200, "OK", null, "Post Created");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // POST api/forum/post/edit
        [HttpPost]
        [Route("post/edit")]
        public JObject EditPost([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Post) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "UPDATE kinship_forum_messages SET message_text = @mess WHERE message_id = @messid";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@messid", SqlDbType.Int);
                        command.Parameters["@messid"].Value = Post["MessageID"];

                        command.Parameters.Add("@mess", SqlDbType.NVarChar);
                        command.Parameters["@mess"].Value = Post["Message"];

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);

                    result = Helper.BuildResult(200, "OK", null, "Post Edited");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // POST api/forum/post/delete
        [HttpPost]
        [Route("post/delete")]
        public JObject DeletePost([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Post) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "UPDATE kinship_forum_messages SET deleted = @delet, reason = @reas WHERE message_id = @messid";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@messid", SqlDbType.NVarChar);
                        command.Parameters["@messid"].Value = Post["MessageID"];

                        command.Parameters.Add("@reas", SqlDbType.NVarChar);
                        command.Parameters["@reas"].Value = Post["Reason"];

                        command.Parameters.Add("@delet", SqlDbType.Bit);
                        command.Parameters["@delet"].Value = 1;

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);

                    result = Helper.BuildResult(200, "OK", null, "Post Deleted");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // GET api/forum/posts/{threadID}
        [HttpGet]
        [Route("posts/{threadID}")]
        public JObject GetPosts([FromHeader]string ClientID, [FromHeader]string ClientSecret, int threadID) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "SELECT message_id, message_text, Acc.account_name, sent_time FROM kinship_forum_messages AS Thread INNER JOIN kinship_accounts AS Acc ON Thread.sender_id = Acc.user_id WHERE thread_id = @tid AND deleted = 0";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@tid", SqlDbType.Int);
                        command.Parameters["@tid"].Value = threadID;

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retMessage = new JObject();
                            retMessage.Add("MessageID", (int)myReader["message_id"]);
                            retMessage.Add("Message", (string)myReader["message_text"]);
                            retMessage.Add("Name", (string)myReader["account_name"]);
                            retMessage.Add("Sent", (DateTime)myReader["sent_time"]);
                            retArr.Add(retMessage);
                            resultsAmount++;
                        }
                    }
                    Helper.CloseDBConnection(dbConnection);
                    JObject retObj = new JObject();
                    retObj.Add("Messages", retArr);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }

        // GET api/forum/categories
        [HttpGet]
        [Route("categories")]
        public JObject GetCategories([FromHeader]string ClientID, [FromHeader]string ClientSecret) {
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
                    dbConnection = Helper.OpenDBConnection("localhost", "kinship");
                    string insertText = "SELECT category_id, title, description FROM kinship_forum_categories";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retCategory = new JObject();
                            retCategory.Add("CategoryID", (int)myReader["category_id"]);
                            retCategory.Add("Title", (string)myReader["title"]);
                            retCategory.Add("Description", (string)myReader["description"]);
                            retArr.Add(retCategory);
                            resultsAmount++;
                        }
                    }
                    Helper.CloseDBConnection(dbConnection);
                    JObject retObj = new JObject();
                    retObj.Add("Categories", retArr);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
    }
}
