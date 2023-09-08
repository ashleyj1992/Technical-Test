namespace Technical_Test.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public override void Clear()
        {
            Console.Clear();
        }

        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
