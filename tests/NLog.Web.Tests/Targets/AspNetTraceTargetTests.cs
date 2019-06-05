using NLog.Web.Targets;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using NLog.Targets;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace NLog.Web.Tests.Targets
{
    public class AspNetTraceTargetTests
    {


        public AspNetTraceTargetTests()
        {

        }

        [Fact]
        public async void TestMethod1()
        {
            // Arrange
            var target = new AspNetTraceTarget();
            var httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
            var traceContextMock = Substitute.For<TraceContext>();
            httpContextAccessorMock.HttpContext.Trace.Returns(traceContextMock);
            target.HttpContextAccessor = httpContextAccessorMock;

            var loggerName = "debugLogger";
            var message = "message1";
            var logEvent = new LogEventInfo(LogLevel.Debug, loggerName, message);

            // Act
            await target.WriteLogEventAsync(logEvent);

            // Assert
            traceContextMock.Received().Write(loggerName, message);
        }


    }
}
