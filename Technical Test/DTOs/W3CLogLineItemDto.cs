using Technical_Test.Models;

namespace Technical_Test.DTOs
{
    public class W3CLogLineItemDto : W3CLogLineItem
    {
        public int HitCount { get; set; }

        public W3CLogLineItemDto()
            :base()
        {
            
        }
    }
}
