using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Test.Extensions
{
    public static class StringExtensions
    {
        public static string GetFirstWord(this string value)
        {
            //return value.Split(' ').First();
            return GetWordByIndex(value, 0, ' ');
        }

        public static string GetWordByIndex(this string value, int index, char separator)
        {
            var values = value.Split(separator);
            return values[index];
        }
    }
}
