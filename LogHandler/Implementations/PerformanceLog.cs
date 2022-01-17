using LogHandler.Interfaces;
using System.Diagnostics;

namespace LogHandler.Implementations
{
    public class PerformanceLog : IPerformanceLog
    {
        public readonly string MethodName = "";
        public readonly string MessageText = "";
        public readonly long Milliseconds = 0;
        public readonly List<string> ExtraVariables = new();
        protected readonly static Stopwatch _stopwatch = new();

        protected PerformanceLog(string methodName, long milliseconds, string messageText = "")
        {
            MethodName = methodName;
            Milliseconds = milliseconds;
            MessageText = messageText;
        }

        public static PerformanceLog CreatePerformanceLog(string methodName, Action function, string messageText = "")
        {
            _stopwatch.Start();
            function();
            var milliseconds = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return new PerformanceLog(methodName, milliseconds, messageText);
        }
        public IPerformanceLog AddLogVariable(string message)
        {
            ExtraVariables.Add(message);
            return this;
        }
    }
}
