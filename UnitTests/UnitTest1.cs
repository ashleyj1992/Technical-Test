using System.Diagnostics;
using Technical_Test.Models;
using Technical_Test.Readers;

namespace UnitTests
{
    
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateReadHeaderInformation()
        {
            var getLogTask = Task.Run(GetTestingLog);
            getLogTask.Wait();

            var log = getLogTask.Result;

            //Check header information is valid
            Assert.AreEqual(1.0, log.Version);
            Assert.AreEqual("Microsoft HTTP Server API 2.0", log.Software);

            var fieldsDictionaryTemp = new Dictionary<int, string>
            {
                { 0, "date" },
                { 1, "time" },
                { 2, "c-ip" },
                { 3, "cs-username" },
                { 4, "s-ip" },
                { 5, "s-port" },
                { 6, "cs-method" },
                { 7, "cs-uri-stem" },
                { 8, "cs-uri-query" },
                { 9, "sc-status" },
                { 10, "cs(User-Agent)" }
            };

            //Check if both dictionarys have the same length and values
            Assert.IsTrue(fieldsDictionaryTemp.SequenceEqual(log.FieldsDictionary));
        }

        [TestMethod]
        public void ValidateReadLogLines()
        {
            var getLogTask = Task.Run(GetTestingLog);
            getLogTask.Wait();

            var log = getLogTask.Result;

            var line1 = new W3CLogLineItem { };
            var line2 = new W3CLogLineItem { };

            Assert.AreEqual(line1, log.LineItems[0]);
            Assert.AreEqual(line2, log.LineItems[1]);
        }

        private async Task<W3cLog> GetTestingLog()
        {
            var testDataFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}Datasets\\logdataunittest.log";
            var reader = new W3CLogFileReader();
            
            return (W3cLog)await reader.Read(testDataFilePath);
        }
    }
}