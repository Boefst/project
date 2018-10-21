using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuthAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            for(int i = 0; i < 3; i++)
            {
                int index = path.LastIndexOf(@"\");
                if (index > 0)
                {
                    path = path.Substring(0, index);
                }
            }

            Console.WriteLine(path);

            Console.WriteLine("Reset database?(y/n)");
            char check = Console.ReadKey().KeyChar;
            if (check == 'y')
            {
                Console.WriteLine("");
                Console.WriteLine("Recreating Database.......");

                string connectionString = "Server=localhost;Database=kinship;Trusted_Connection=true";
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                string[] structurePaths = Directory.GetFiles(@"C:\Users\Ander\Desktop\Kinship\Kinship\Database\sql\structure", "*.sql", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < structurePaths.Length; i++) {
                    Console.WriteLine(structurePaths[i]);

                    string structureScript = File.ReadAllText($@"{structurePaths[i]}");
                    IEnumerable<string> structureCommandStrings = Regex.Split(structureScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    foreach (string commandString in structureCommandStrings) {
                        if (commandString.Trim() != "") {
                            using (var command = new SqlCommand(commandString, myConnection)) {
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                
                string[] dataPaths = Directory.GetFiles(@"C:\Users\Ander\Desktop\Kinship\Kinship\Database\sql\data\", "*.sql", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < dataPaths.Length; i++) {
                    Console.WriteLine(dataPaths[i]);

                    string dataScript = File.ReadAllText($@"{dataPaths[i]}");
                    IEnumerable<string> dataCommandStrings = Regex.Split(dataScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    foreach (string commandString in dataCommandStrings) {
                        if (commandString.Trim() != "") {
                            using (var command = new SqlCommand(commandString, myConnection)) {
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }

                myConnection.Close();
                Console.WriteLine("Database is ready!");
            }
            
            // Start the WebHost
            Console.WriteLine("");
            Console.WriteLine("Starting PaymentSolutionHub");
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
