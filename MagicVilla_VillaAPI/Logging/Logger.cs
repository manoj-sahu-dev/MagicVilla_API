using System;
namespace MagicVilla_VillaAPI.Logging
{
    public class Logger : ILogging
    {
        public void Log(string message, LogLevel logLevel = LogLevel.INFO)
        {
            var data = logLevel.ToString() + " : " + message;
            Console.WriteLine(data);
        }
    }
}

