using Technical_Test.Extensions;
using Technical_Test.Loggers;
using Technical_Test.Models;
using Technical_Test.Readers;

internal class Program
{
    private static void Main(string[] args)
    {
        var testDataFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}Datasets\\logdata.log";

        var logger = new ConsoleLogger();
        var reader = new W3CLogFileReader();

        var logReadTask = reader.Read(testDataFilePath);
        logReadTask.Wait();

        var log = (W3cLog)logReadTask.Result;

        var isRunning = true;
        while(isRunning)
        {
            logger.Clear();
            logger.Log("Press 1 to view distinct results ranked by hit count with header information");
            logger.Log("Press 2 to view distinct results ranked by hit count without header information");
            logger.Log("Press 3 to view all results ranked by hit count");
            logger.Log("Press 4 to quit");
            var readKey = Console.ReadKey(true);

            switch(readKey.KeyChar)
            {
                case '1':
                    {
                        log.LogToConsole(logger);
                        break;
                    }

                case '2':
                    {
                        //log.LogToConsole(logger);
                        break;
                    }

                case '3':
                    {
                        break;
                    }

                case '4':
                    {
                        isRunning = false;
                        break;
                    }
            }

            Console.ReadKey();
        }
    }
}