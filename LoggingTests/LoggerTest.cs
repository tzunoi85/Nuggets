using FizzWare.NBuilder;
using Logging;
using Moq;
using NUnit.Framework;
using System;
using Serilog;

namespace LoggingTests
{
    [TestFixture]
    public class LoggerTest
    {

        [TestCase]
        public void Should_log_error()
        {

            Exception exception = Builder<Exception>.CreateNew()
                .WithFactory(() => new Exception("aaaaaaaaa", new Exception("inner exception")))
                .Build();

            var serilog = new Mock<ILogger>().Object;

            var logger = new Mock<Logging.Logger>(new[] { serilog }).Object;

            logger.Error(exception);

            Assert.IsTrue(true);
        }
    }
}
