using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Test.Loggers
{
    public abstract class ILogger
    {
        public abstract void Log(string message);
        public abstract void Clear();
    }
}
