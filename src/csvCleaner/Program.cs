using System;
using System.IO;
using System.Linq;

namespace csvCleaner
{
    internal class Program
    {
        private static void init(string[] args, out string urlData, out string urlConfig)
        {
            urlData = null;
            urlConfig = null;


            if (args.Count() > 1)
            {
                urlData = args[0];
            }

            if (args.Count() > 2)
            {
                urlConfig = args[1];
            }



            urlData = urlData ?? $@"{Directory.GetCurrentDirectory()}\data.csv";
            urlConfig = urlConfig ?? $@"{Directory.GetCurrentDirectory()}\complete.json";
        }

        private static async void Main(string[] args)
        {

            init(args, out string urlData, out string urlConfig);

            var url = urlData.Replace(urlData.Split('\\').Last(), "");


            var errorFile = $@"{url}\error.log";
            var resultFile = $@"{url}\result.csv";

            var configFile = System.IO.File.ReadAllLines(urlConfig);
            var content = System.IO.File.ReadAllLines(urlData);


            var Helper = new App.CsvHelper();

            var config = Helper.CreateConfig().ConvertConfig(configFile);
            var data = Helper.CreateHelper(config).ConvertData(content);
            var result = await Helper.CreateHelper(config).Generate(data);

            System.IO.File.WriteAllLines(resultFile, result.Data);
            System.IO.File.WriteAllLines(errorFile, result.Errors);


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************************");
            Console.WriteLine("**************************                                               ***********************");
            Console.WriteLine("**************************            the code be with you               ***********************");
            Console.WriteLine("**************************            https://muxocode.com               ***********************");
            Console.WriteLine("**************************                                               ***********************");
            Console.WriteLine("************************************************************************************************");

            Console.ReadKey();
        }
    }
}