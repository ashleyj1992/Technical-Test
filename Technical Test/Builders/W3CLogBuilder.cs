using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Test.DTOs;
using Technical_Test.Loggers;
using Technical_Test.Models;

namespace Technical_Test.Builders
{
    public class W3CLogBuilder
    {
        private List<W3CLogLineItemDto> _lineItemDtos;
        public W3cLog Log { get; set; }

        public W3CLogBuilder(W3cLog log)
        {
            _lineItemDtos = new List<W3CLogLineItemDto>();
            Log = log;
        }

        public W3CLogBuilder Build()
        {
            _lineItemDtos = Log.LineItems
                .Select(x => new W3CLogLineItemDto
                {
                    HitCount = Log.LineItems.Count(y => y.CSURIStem == x.CSURIStem),
                    ClientIP = x.ClientIP,
                    CSMethod = x.CSMethod,
                    CSURIStem = x.CSURIStem,
                    CSUsername = x.CSUsername,
                    DateTimeCreated = x.DateTimeCreated,
                    ServerIP = x.ServerIP,
                    ServerPort = x.ServerPort
                })
                .ToList();

            return this;
        }

        public List<W3CLogLineItemDto> GetByRankedHits()
        {
            var distinctItems = _lineItemDtos
                .GroupBy(x => x.CSURIStem)
                .Select(x => x.First())
                .ToList();

            return distinctItems.OrderByDescending(x => x.DateTimeCreated).ThenBy(x => x.HitCount).ToList();
        }
    }
}
