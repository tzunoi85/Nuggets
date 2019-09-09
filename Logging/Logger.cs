using System;
using System.Text;
using ISerilog = Serilog.ILogger;

using Common.Loggers;


namespace Logging
{
    public class Logger
        : ILogger
    {
        private ISerilog _logger { get; }

        public Logger(ISerilog logger)
            => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public void Debug(string message)
            => _logger.Debug(message);

        public void Error(Exception exception)
            => _logger.Error(CreateMessage(exception));

        public void Information(string message)
            => _logger.Information(message);

        public void Warning(string message)
            => _logger.Warning(message);

        private string CreateMessage(Exception exception)
        {
            Exception localException = exception;
            var result = new StringBuilder();
            var level = 1;

            while (localException != null)
            {
                result.AppendLine($"Message #{level++}: { localException.Message}");
                result.AppendLine($"========================================================================================");
                localException = localException.InnerException;
            }

            result.AppendLine($"Stack trace: { exception.StackTrace}");

            return result.ToString();
        }
    }
}
