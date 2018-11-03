using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using APIHelper;

namespace KinshipAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase {
        Helper Helper = new Helper();

        // POST api/support/ticket/create
        [HttpPost]
        [Route("ticket/create")]
        public JObject CreateTicket([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Ticket) {
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
                    string insertText = "EXEC procedure_create_ticket @title = @tit, @category = @cat, @world = @wo, @senderID = @send, @message = @mes, @isAdmin = @ia";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@tit", SqlDbType.NVarChar);
                        command.Parameters["@tit"].Value = Ticket["Title"];

                        command.Parameters.Add("@cat", SqlDbType.NVarChar);
                        command.Parameters["@cat"].Value = Ticket["Category"];

                        command.Parameters.Add("@wo", SqlDbType.NVarChar);
                        command.Parameters["@wo"].Value = Ticket["World"];

                        command.Parameters.Add("@send", SqlDbType.Int);
                        command.Parameters["@send"].Value = userID;

                        command.Parameters.Add("@mes", SqlDbType.NVarChar);
                        command.Parameters["@mes"].Value = Ticket["Message"];

                        command.Parameters.Add("@ia", SqlDbType.Bit);
                        command.Parameters["@ia"].Value = 0;

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                        Helper.CloseDBConnection(dbConnection);

                        result = Helper.BuildResult(200, "OK", null, "Session Found");
                    }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
        // POST api/support/ticket/rate
        [HttpPost]
        [Route("ticket/rate")]
        public JObject RateTicket([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Rating) {
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
                    string insertText = "UPDATE kinship_support_tickets SET rating = @rate WHERE ticket_id = @ticket";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@rate", SqlDbType.Int);
                        command.Parameters["@rate"].Value = Rating["Rating"];

                        command.Parameters.Add("@ticket", SqlDbType.Int);
                        command.Parameters["@ticket"].Value = Rating["Ticket"];

                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);
                    result = Helper.BuildResult(200, "OK", null, "Session Found");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
        // POST api/support/ticket/message
        [HttpPost]
        [Route("ticket/message")]
        public JObject AddMessage([FromHeader]string ClientID, [FromHeader]string ClientSecret, [FromBody]JObject Message) {
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
                    string insertText = "INSERT INTO kinship_support_messages (ticket_id, ticket_message, sender_id, admin, sent_time) VALUES(@ticket, @message, @send, 0, CURRENT_TIMESTAMP)";
                    using (SqlCommand command = new SqlCommand(insertText, dbConnection)) {
                        command.Parameters.Add("@ticket", SqlDbType.Int);
                        command.Parameters["@ticket"].Value = Message["Ticket"];

                        command.Parameters.Add("@message", SqlDbType.NVarChar);
                        command.Parameters["@message"].Value = Message["Message"];

                        command.Parameters.Add("@send", SqlDbType.Int);
                        command.Parameters["@send"].Value = userID;
                        
                        SqlDataReader myReader = command.ExecuteReader();
                    }
                    Helper.CloseDBConnection(dbConnection);
                    result = Helper.BuildResult(200, "OK", null, "Session Found");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
        // GET api/support/tickets
        [HttpGet]
        [Route("tickets")]
        public JObject GetTickets([FromHeader]string ClientID, [FromHeader]string ClientSecret) {
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

                    string commandText = "SELECT ticket_id, ticket_title, ticket_category, ticket_world, create_time, rating, ticket_status FROM kinship_support_tickets WHERE sender_id = @id";
                    using (SqlCommand command = new SqlCommand(commandText, dbConnection)) {
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = userID;

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retTicket = new JObject();
                            retTicket.Add("TicketID", (int)myReader["ticket_id"]);
                            retTicket.Add("Title", (string)myReader["ticket_title"]);
                            retTicket.Add("Category", (string)myReader["ticket_category"]);
                            retTicket.Add("World", (string)myReader["ticket_world"]);
                            retTicket.Add("Created", (DateTime)myReader["create_time"]);
                            retTicket.Add("Status", (string)myReader["ticket_status"]);
                            if (myReader["rating"] == DBNull.Value) {
                                retTicket.Add("Rating", "");
                            }
                            else {
                                retTicket.Add("Rating", (int)myReader["rating"]);
                            }
                            retArr.Add(retTicket);
                            resultsAmount++;
                        }
                    }
                    Helper.CloseDBConnection(dbConnection);
                    JObject retObj = new JObject();
                    retObj.Add("Tickets", retArr);
                    result = Helper.BuildResult(200, "OK", retObj, "");
                }
                catch (Exception e) {
                    result = Helper.BuildResult(500, "Internal Server Error", null, e.Message);
                }
            }
            return result;
        }
        // GET api/support/ticket/message
        [HttpGet]
        [Route("ticket/conversation/{ticketID}")]
        public JObject GetMessages([FromHeader]string ClientID, [FromHeader]string ClientSecret, int ticketID) {
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

                    string commandText = "EXEC [kinship].[dbo].[procedure_get_ticket_conversation] @id = @ticketid";
                    using (SqlCommand command = new SqlCommand(commandText, dbConnection)) {
                        command.Parameters.Add("@ticketid", SqlDbType.Int);
                        command.Parameters["@ticketid"].Value = ticketID;

                        SqlDataReader myReader = command.ExecuteReader();
                        int resultsAmount = 0;
                        while (myReader.Read()) {
                            JObject retMessage = new JObject();
                            retMessage.Add("MessageID", (int)myReader["message_id"]);
                            retMessage.Add("Message", (string)myReader["ticket_message"]);
                            retMessage.Add("Name", (string)myReader["account_name"]);
                            retMessage.Add("Time", (DateTime)myReader["sent_time"]);
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
    }
}
