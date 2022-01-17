namespace LogHandler.Implementations
{
    public class PerformanceLogGeneric<T> : PerformanceLog
    {

        public readonly T ReturnValue;

        private PerformanceLogGeneric(T returnValue, string methodName, long milliseconds, string messageText = "")
            : base(methodName, milliseconds, messageText)
        {
            ReturnValue = returnValue;
        }

        public static PerformanceLogGeneric<T> CreatePerformanceLog(string methodName, Func<T> function, string messageText = "")
        {
            _stopwatch.Start();
            var returnValue = function();
            var milliseconds = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return new PerformanceLogGeneric<T>(returnValue, methodName, milliseconds, messageText);
        }
    }
}
