using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog.Targets;

namespace NLog.Web.Tests
{
    internal static class TargetExtensions
    {
        /// <summary>
        /// Write async awaitable
        /// </summary>
        /// <returns></returns>
        public static Task WriteLogEventAsync(this Target target, LogEventInfo logEvent)
        {
            var @event = new ManualResetEvent(false);

            target.WriteAsyncLogEvent(logEvent.WithContinuation(exception => @event.Set()));

            @event.WaitOne(TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }
    }
}
