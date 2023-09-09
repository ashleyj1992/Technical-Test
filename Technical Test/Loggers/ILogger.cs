namespace Technical_Test.Loggers
{
    public abstract class ILogger
    {
        public abstract void Log(string message);
        public abstract void Clear();
    }
}
