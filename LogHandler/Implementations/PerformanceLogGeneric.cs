namespace LogHandler.Implementations
{
    public class PerformanceLog<T> : PerformanceLog
    {

        public readonly T ReturnValue;

        private PerformanceLog(T returnValue, string methodName, long milliseconds, string messageText = "")
            : base(methodName, milliseconds, messageText)
        {
            ReturnValue = returnValue;
        }

        public static PerformanceLog<T> CreatePerformanceLog(string methodName, Func<T> function, string messageText = "")
        {
            _stopwatch.Start();
            var returnValue = function();
            var milliseconds = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return new PerformanceLog<T>(returnValue, methodName, milliseconds, messageText);
        }
    }
}
