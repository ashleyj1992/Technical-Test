namespace Technical_Test.Models
{
    public class W3cLog : ILog
    {
        /// <summary>
        /// Fields here are based on the assumptions I made from the example data given
        /// </summary>
        public string Software { get; set; }
        public double Version { get; set; }
        public DateTimeOffset DateTimeCreated { get; set; }
        public Dictionary<int, string> FieldsDictionary{ get; set; }
        public List<W3CLogLineItem> LineItems { get; set; }

        public W3cLog()
        {
            Software = string.Empty;
            Version = double.NaN;
            DateTimeCreated = DateTimeOffset.MinValue;
            FieldsDictionary = new Dictionary<int, string>();
            LineItems = new List<W3CLogLineItem>();
        }
    }
}
