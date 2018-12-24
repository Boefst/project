using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace APIHelper {
    public class DBreset {
        public static void Main(string[] args) {

            dbresetfunc("kinship",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship\structure",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship\procedure",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship\data");
            dbresetfunc("kinship_admin",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship_admin\structure",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship_admin\procedure",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship_admin\data");
            dbresetfunc("kinship_world0",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship_world0\structure",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship_world0\procedure",
                @"C:\Users\Ander\Desktop\Kinship\Kinship\Database\kinship_world0\data");

            Console.WriteLine("Database is ready!");
            Console.WriteLine("Starting Kinship");
        }

        public static void dbresetfunc(string db, string structure, string procedure, string data) {
            Console.WriteLine("");
            Console.WriteLine($"Recreating Database {db}.......");

            string kinshipConnection = $"Server=localhost;Database={db};Trusted_Connection=true";
            SqlConnection myConnection = new SqlConnection(kinshipConnection);
            myConnection.Open();

            string[] structurePaths = Directory.GetFiles(structure, "*.sql", SearchOption.AllDirectories);
            string[] procedurePaths = Directory.GetFiles(procedure, "*.sql", SearchOption.AllDirectories);
            string[] dataPaths = Directory.GetFiles(data, "*.sql", SearchOption.AllDirectories);
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
        }
    }
}
