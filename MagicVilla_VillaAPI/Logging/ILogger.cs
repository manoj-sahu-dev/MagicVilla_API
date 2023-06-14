using System;
namespace MagicVilla_VillaAPI.Logging
{
    public interface ILogging
    {
        void Log(string message, LogLevel logLevel = LogLevel.INFO);
    }
}

