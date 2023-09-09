using Technical_Test.Builders;
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
            logger.Log("Press 1 to view all results ranked by hit count ascending");
            logger.Log("Press 2 to view all results ranked by hit count then date time ascending");
            logger.Log("Press 3 to view header information");
            logger.Log("Press 4 to quit");

            var readKey = Console.ReadKey(true);

            switch(readKey.KeyChar)
            {
                case '1':
                    {
                        var builder = new W3CLogBuilder(log).Build();
                        var allRankedByHits = builder.GetAllByRankedHitsAsc();

                        foreach (var lineItem in allRankedByHits)
                        {
                            var message = $"URL: {lineItem.CSURIStem} Hits: {lineItem.HitCount}";
                            logger.Log(message);
                        }
                        break;
                    }

                case '2':
                    {
                        var builder = new W3CLogBuilder(log).Build();
                        var allRankedByHitsAndDateTime = builder.GetAllByRankedRecentHitsAsc();

                        foreach (var lineItem in allRankedByHitsAndDateTime)
                        {
                            var message = $"URL: {lineItem.CSURIStem} Hits: {lineItem.HitCount} Date: {lineItem.DateTimeCreated}";
                            logger.Log(message);
                        }
                        break;
                    }

                case '3':
                    {
                        logger.Log($"Software: {log.Software}");
                        logger.Log($"Version: {log.Version.ToString("N2")}");
                        logger.Log($"Date created: {log.DateTimeCreated}");

                        foreach (var field in log.FieldsDictionary)
                        {
                            logger.Log($"Field #{field.Key} {field.Value}");
                        }
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