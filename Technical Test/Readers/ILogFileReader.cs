using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Test.Models;

namespace Technical_Test.Readers
{
    public interface ILogFileReader
    {
        public Task<ILog> Read(string fileName);
    }
}
