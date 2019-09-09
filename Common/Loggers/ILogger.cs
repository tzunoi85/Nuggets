using System;


namespace Common.Loggers
{
    public interface ILogger
    {
        void Debug(string message);

        void Information(string message);

        void Error(Exception exception);

        void Warning(string message);
    }
}
