using System.Diagnostics;
using System.Globalization;
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
            CultureInfo cultureInfo = new CultureInfo("en-US");

            var getLogTask = Task.Run(GetTestingLog);
            getLogTask.Wait();

            var log = getLogTask.Result;

            var line1 = new W3CLogLineItem {
                DateTimeCreated = DateTime.Parse("2005-01-05 17:42:15", cultureInfo),
                ClientIP = "172.11.255.255",
                CSUsername = "-",
                ServerIP = "172.30.255.255",
                ServerPort = 80,
                CSMethod = "GET",
                CSURIStem = "/images/picture2.jpg",
                CSURIQuery = "-",
                SCStatus = "400",
                CSUserAgent = "Mozilla/4.0+"
            };
            var line2 = new W3CLogLineItem {
                DateTimeCreated = DateTime.Parse("2002-06-01 10:12:11", cultureInfo),
                ClientIP = "172.22.255.255",
                CSUsername = "-",
                ServerIP = "172.22.255.255",
                ServerPort = 80,
                CSMethod = "GET",
                CSURIStem = "/images/picture.jpg",
                CSURIQuery = "-",
                SCStatus = "200",
                CSUserAgent = "Mozilla/4.0+"
            };

            var actualLine1 = log.LineItems[0];
            var actualLine2 = log.LineItems[1];

            //Check line 1
            Assert.AreEqual(line1.DateTimeCreated, actualLine1.DateTimeCreated);
            Assert.AreEqual(line1.ClientIP, actualLine1.ClientIP);
            Assert.AreEqual(line1.CSUsername, actualLine1.CSUsername);
            Assert.AreEqual(line1.ServerIP, actualLine1.ServerIP);
            Assert.AreEqual(line1.ServerPort, actualLine1.ServerPort);
            Assert.AreEqual(line1.CSMethod, actualLine1.CSMethod);
            Assert.AreEqual(line1.CSURIQuery, actualLine1.CSURIQuery);
            Assert.AreEqual(line1.SCStatus, actualLine1.SCStatus);
            Assert.AreEqual(line1.CSUserAgent, actualLine1.CSUserAgent);

            //Check line 2
            Assert.AreEqual(line2.DateTimeCreated, actualLine2.DateTimeCreated);
            Assert.AreEqual(line2.ClientIP, actualLine2.ClientIP);
            Assert.AreEqual(line2.CSUsername, actualLine2.CSUsername);
            Assert.AreEqual(line2.ServerIP, actualLine2.ServerIP);
            Assert.AreEqual(line2.ServerPort, actualLine2.ServerPort);
            Assert.AreEqual(line2.CSMethod, actualLine2.CSMethod);
            Assert.AreEqual(line2.CSURIQuery, actualLine2.CSURIQuery);
            Assert.AreEqual(line2.SCStatus, actualLine2.SCStatus);
            Assert.AreEqual(line2.CSUserAgent, actualLine2.CSUserAgent);
        }

        private async Task<W3cLog> GetTestingLog()
        {
            var testDataFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}Datasets\\logdataunittest.log";
            var reader = new W3CLogFileReader();
            
            return (W3cLog)await reader.Read(testDataFilePath);
        }
    }
}