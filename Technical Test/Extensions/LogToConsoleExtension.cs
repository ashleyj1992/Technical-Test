using Technical_Test.Builders;
using Technical_Test.DTOs;
using Technical_Test.Loggers;
using Technical_Test.Models;

namespace Technical_Test.Extensions
{
    public static class LogToConsoleExtension
    {
        public static void LogToConsole(
            this W3cLog log, 
            ILogger logger)
        {
            var lineItemDtos = log.LineItems
                .Select(x => new W3CLogLineItemDto { 
                    HitCount = log.LineItems.Count(y => y.CSURIStem == x.CSURIStem), 
                    ClientIP = x.ClientIP, 
                    CSMethod = x.CSMethod, 
                    CSURIStem = x.CSURIStem, 
                    CSUsername = x.CSUsername, 
                    DateTimeCreated = x.DateTimeCreated, 
                    ServerIP = x.ServerIP, 
                    ServerPort = x.ServerPort })
                .ToList();

            logger.Clear();
            logger.Log($"Software: {log.Software}");
            logger.Log($"Version: {log.Version.ToString("N2")}");
            logger.Log($"Date created: {log.DateTimeCreated}");

            foreach(var field in log.FieldsDictionary)
            {
                logger.Log($"Field #{field.Key} {field.Value}");
            }

            logger.Log(Environment.NewLine);

            //Assuming we want to only show a list of ranked URL's by their hit count

            var distinctItems = lineItemDtos
                .GroupBy(x => x.CSURIStem)
                .Select(x => x.First())
                .ToList();

            foreach (var lineItem in distinctItems.OrderByDescending(x => x.DateTimeCreated).ThenBy(x => x.HitCount))
            {
                var message = $"URL: {lineItem.CSURIStem} Hits: {lineItem.HitCount}";
                logger.Log(message);
            }
        }

        public static void LogTest(this W3CLogBuilder builder, ILogger logger)
        {
            var build = builder.Build();

        }
    }
}
