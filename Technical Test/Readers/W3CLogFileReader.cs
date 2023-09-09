using System.Globalization;
using Technical_Test.Extensions;
using Technical_Test.Models;

namespace Technical_Test.Readers
{
    public class W3CLogFileReader : ILogFileReader
    {
        private W3cLog _log;
        private CultureInfo _selectedCulture;

        public W3CLogFileReader()
        {
            _log = new W3cLog();

            //Based on the example data the date time zones are American
            _selectedCulture = new CultureInfo("en-US");
        }

        public async Task<ILog> Read(string fileName)
        {
            using(var stream =  new StreamReader(fileName))
            {
                //Assuming we have 4 lines of header information in the file
                for(int i = 0; i < 4; i++)
                {
                    var currentLine = await stream.ReadLineAsync();
                    if(currentLine != null)
                    {
                        SetLogFieldData(currentLine);
                    }
                }

                var lineItems = new List<W3CLogLineItem>();

                while (!stream.EndOfStream)
                {
                    var currentLine = await stream.ReadLineAsync();
                    if(currentLine != null)
                    {
                        //Assuming that these are the only fields for this type of log, can easily be updated
                        var cipIndex = _log.FieldsDictionary.Single(x => x.Value == "c-ip").Key;
                        var csUsernameIndex = _log.FieldsDictionary.Single(x => x.Value == "cs-username").Key;
                        var sPortIndex = _log.FieldsDictionary.Single(x => x.Value == "s-port").Key;
                        var csMethodIndex = _log.FieldsDictionary.Single(x => x.Value == "cs-method").Key;
                        var csUriQuery = _log.FieldsDictionary.Single(x => x.Value == "cs-uri-query").Key;
                        var csuristemIndex = _log.FieldsDictionary.Single(x => x.Value == "cs-uri-stem").Key;
                        var sIpIndex = _log.FieldsDictionary.Single(x => x.Value == "s-ip").Key;
                        var dateIndex = _log.FieldsDictionary.Single(x => x.Value == "date").Key;
                        var timeIndex = _log.FieldsDictionary.Single(x => x.Value == "time").Key;

                        var dateTimeStr = $"{currentLine.GetWordByIndex(dateIndex, ' ')} {currentLine.GetWordByIndex(timeIndex, ' ')}";

                        lineItems.Add(new W3CLogLineItem
                        {
                            DateTimeCreated = DateTimeOffset.Parse(dateTimeStr, _selectedCulture),
                            ClientIP = currentLine.GetWordByIndex(cipIndex, ' '),
                            CSUsername = currentLine.GetWordByIndex(csUsernameIndex, ' '),
                            ServerIP = currentLine.GetWordByIndex(sIpIndex, ' '),
                            ServerPort = Convert.ToInt32(currentLine.GetWordByIndex(sPortIndex, ' ')),
                            CSURIStem = currentLine.GetWordByIndex(csuristemIndex, ' '), 
                            CSMethod = currentLine.GetWordByIndex(csMethodIndex, ' '), 
                            CSURIQuery = currentLine.GetWordByIndex(csUriQuery, ' ')
                        });
                    }
                }

                _log.LineItems = lineItems;
            }

            return _log;
        }

        public void SetLogFieldData(string line)
        {
            var firstWordOfLine = line.GetFirstWord().ToLower();
            switch(firstWordOfLine)
            {
                case "#software:":
                    {
                        _log.Software = line.Remove(0, firstWordOfLine.Length).Trim();
                        break;
                    }

                case "#version:":
                    {
                        _log.Version = Convert.ToDouble(line.GetWordByIndex(1, ' '));
                        break;
                    }

                case "#date:":
                    {
                        var dateTimeStr = $"{line.GetWordByIndex(1, ' ')} {line.GetWordByIndex(2, ' ')}";
                        _log.DateTimeCreated = DateTimeOffset.Parse(dateTimeStr, _selectedCulture);
                        break;
                    }

                case "#fields:":
                    {
                        var list = line.Split(' ').ToList();
                        //Remove first field name header
                        list.RemoveAt(0);
                        _log.FieldsDictionary = new Dictionary<int, string>();
                        int count = 0;
                        foreach(var item in list)
                        {
                            var value = item.Trim();
                            _log.FieldsDictionary.Add(count, value);
                            count++;
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
    }
}
