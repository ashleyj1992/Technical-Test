using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Test.Models
{
    public class W3CLogLineItem
    {
        /// <summary>
        /// Assumptions are made that these are the required fields for the log file dataset
        /// </summary>
        public DateTimeOffset DateTimeCreated { get; set; }
        public string ClientIP { get; set; }
        public string CSUsername { get; set; }
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
        public string CSMethod { get; set; }
        public string CSURIStem { get; set; }
        public string CSURIQuery { get; set; }
        public string SCStatus { get; set; }

        public W3CLogLineItem()
        {
            DateTimeCreated = DateTimeOffset.MinValue;
            ClientIP = string.Empty;
            CSUsername = string.Empty;
            ServerIP = string.Empty;
            ServerPort = int.MinValue;
            CSMethod = string.Empty;
            CSURIStem = string.Empty;
            CSURIQuery = string.Empty;
            SCStatus = string.Empty;
        }
    }
}
