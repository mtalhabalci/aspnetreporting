using System;
using System.Threading.Tasks;

namespace Rise.Migrator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Migrator migrator = new Migrator();
            var dbSettings = migrator.GetDbSettings();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{DateTime.Now} | Host Database: {dbSettings.ConnectionString}");
            Console.WriteLine($"{DateTime.Now} | Continue to migration for this host database and all seed datas... (Y/N)");
            if (Console.ReadLine() != "Y")
            {
                Console.WriteLine($"{DateTime.Now} | Migration canceled...");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now} | HOST database migration started...");

            if (!await migrator.EnsureCreated())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{DateTime.Now} | Database already exist.So Seeder can duplicate data.");
                Console.WriteLine($"{DateTime.Now} | Continue to migration for all seed datas... (Y/N)");
                if (Console.ReadLine() != "Y")
                {
                    Console.WriteLine($"{DateTime.Now} | Seeder canceled...");
                    Console.ReadKey();
                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now} | HOST database migration completed.");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{DateTime.Now} | ----------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now} | HOST database default seed creation started...");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now} | HOST database default seed creation completed.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{DateTime.Now} | ----------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"{DateTime.Now} | All database have been migrated.");
            Console.ReadKey();
        }
    }
}
