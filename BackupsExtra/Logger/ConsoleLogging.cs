using System;

namespace BackupsExtra.Logger
{
    public class ConsoleLogging : ILogger
    {
        public void Logger(string message)
        {
            Console.WriteLine(DateTime.Now + message);
        }
    }
}