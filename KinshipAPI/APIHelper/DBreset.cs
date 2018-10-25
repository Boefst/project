using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace APIHelper {
    public class DBreset {
        public static void Main(string[] args) {

            Console.WriteLine("");
            Console.WriteLine("Recreating Database.......");

            string kinshipConnection = "Server=localhost;Database=kinship;Trusted_Connection=true";
            SqlConnection myConnection = new SqlConnection(kinshipConnection);
            myConnection.Open();

            string[] structurePaths = Directory.GetFiles(@"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship\structure", "*.sql", SearchOption.AllDirectories);
            string[] procedurePaths = Directory.GetFiles(@"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship\procedure", "*.sql", SearchOption.AllDirectories);
            string[] dataPaths = Directory.GetFiles(@"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship\data", "*.sql", SearchOption.AllDirectories);
            string[] tempPaths = structurePaths.Concat(procedurePaths).ToArray();
            string[] combinedPaths = tempPaths.Concat(dataPaths).ToArray();

            for (int i = 0; i < combinedPaths.Length; i++) {
                Console.WriteLine(combinedPaths[i]);

                string structureScript = File.ReadAllText($@"{combinedPaths[i]}");
                IEnumerable<string> structureCommandStrings = Regex.Split(structureScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (string commandString in structureCommandStrings) {
                    if (commandString.Trim() != "") {
                        using (var command = new SqlCommand(commandString, myConnection)) {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            myConnection.Close();

            /*string world0Connection = "Server=localhost;Database=world0;Trusted_Connection=true";
            SqlConnection mynewConnection = new SqlConnection(world0Connection);
            mynewConnection.Open();
            mynewConnection.Close();*/

            Console.WriteLine("Database is ready!");
            Console.WriteLine("Starting PaymentSolutionHub");
        }
    }
}
