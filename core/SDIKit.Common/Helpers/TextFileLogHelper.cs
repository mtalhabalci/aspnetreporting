using SDIKit.Common.Enums;

using System;
using System.IO;

namespace SDIKit.Common.Helpers
{
    public static class TextFileLogHelper
    {
        public static void StartupLog(string message)
        {
            var directory = Directory.CreateDirectory(Path.Combine(System.AppContext.BaseDirectory, $"{nameof(StartupLog)}s"));
            var logFile = $"log-{DateTime.Now.Date.ToString("ddMMyyyy")}-{Guid.NewGuid()}.txt";
            var fullPath = Path.Combine(directory.FullName, logFile);
            File.AppendAllText(fullPath, message);
        }

        public static void FallbackLog(string message)
        {
            var directory = Directory.CreateDirectory(Path.Combine(System.AppContext.BaseDirectory, "fallback_logs"));
            var logFile = $"log-{DateTime.Now.Date.ToString("ddMMyyyy")}-{Guid.NewGuid()}.txt";
            var fullPath = Path.Combine(directory.FullName, logFile);
            File.AppendAllText(fullPath, message);
        }

        public static void EventLog(ApplicationSandbox sandbox, string message)
        {
            var directory = Directory.CreateDirectory(Path.Combine(System.AppContext.BaseDirectory, $"{nameof(EventLog)}s"));
            var logFile = $"{sandbox.ToString()}-log-{DateTime.Now.Date.ToString("ddMMyyyy")}-{Guid.NewGuid()}.txt";
            var fullPath = Path.Combine(directory.FullName, logFile);
            File.AppendAllText(fullPath, message);
        }
    }
}