using System;
using System.IO;
using Backups.Entities;

namespace BackupsExtra.Logger
{
    public class FileLogging : ILogger
    {
        public void Logger(string message)
        {
            // File.AppendAllText("logg.txt", DateTime.Now + message);
        }
    }
}