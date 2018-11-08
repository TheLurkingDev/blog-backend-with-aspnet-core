using Contracts;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private const string logFileName = "\\appLog.txt";
        private string _logFile;

        public LoggerManager()
        {
            _logFile = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            _logFile += logFileName;
            Log.Logger = new LoggerConfiguration().WriteTo.File(_logFile).CreateLogger();
        }

        public static void AddToLoggerFactory(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
        }

        public void LogDebug(string message)
        {
            Log.Logger.Debug(message);
        }

        public void LogError(string message)
        {
            Log.Logger.Error(message);
        }

        public void LogInfo(string message)
        {
            Log.Logger.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Logger.Warning(message);
        }
    }
}
