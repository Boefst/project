using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIHelper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KinshipAPI {
    public class Program {
        public static void Main(string[] args) {
            string path = Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++) {
                int index = path.LastIndexOf(@"\");
                if (index > 0) {
                    path = path.Substring(0, index);
                }
            }

            Console.WriteLine(path);
            Console.WriteLine("Reset database?(y/n)");
            char check = Console.ReadKey().KeyChar;
            if (check == 'y') {
                DBreset.Main(args);
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
