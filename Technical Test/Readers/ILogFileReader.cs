using Technical_Test.Models;

namespace Technical_Test.Readers
{
    public interface ILogFileReader
    {
        public Task<ILog> Read(string fileName);
    }
}
