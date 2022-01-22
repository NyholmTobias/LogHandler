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

        /// <summary>
        /// private constructor, use CreatePerformanceLog instead.
        /// </summary>
        /// <param name="methodName">The name of the method that is beeing checked for performance.</param>
        /// <param name="milliseconds">The number of milliseconds it took to run the passed action parameter.</param>
        /// <param name="messageText">Specific message.</param>
        private PerformanceLog(string methodName, long milliseconds, string messageText = "")
        {
            MethodName = methodName;
            Milliseconds = milliseconds;
            MessageText = messageText;
        }

        /// <summary>
        /// Creates and instanse of PerformanceLog, gets call when there is no return value from the passed method.
        /// </summary>
        /// <param name="methodName">The name of the method that is beeing checked for performance.</param>
        /// <param name="function">The method that is getting checked for performance.</param>
        /// <param name="messageText">Specific message.</param>
        /// <returns>A new instance of PerformanceLog</returns>
        public static PerformanceLog CreatePerformanceLog(string methodName,
                                                          Action function,
                                                          string messageText = "")
        {
            _stopwatch.Start();
            function();
            var milliseconds = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return new PerformanceLog(methodName, milliseconds, messageText);
        }

        /// <summary>
        /// Creates and instanse of PerformanceLog and an return value, gets call when there is a return value from the passed method.
        /// </summary>
        /// <typeparam name="T">The type of the returnValue</typeparam>
        /// <param name="methodName">The name of the method that is beeing checked for performance.</param>
        /// <param name="function">The method that is getting checked for performance.</param>
        /// <param name="messageText">Specific message.</param>
        /// <returns>A tuple including a new instance of performanceLog and the value thats returned from the passed method.</returns>
        public static (PerformanceLog log, T returnValue) CreatePerformanceLog<T>(string methodName,
                                                                                  Func<T> function,
                                                                                  string messageText = "")
        {
            _stopwatch.Start();
            var returnValue = function();
            var milliseconds = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Reset();
            return (new PerformanceLog(methodName, milliseconds, messageText), returnValue);
        }

        /// <summary>
        /// Adds a custom string property to the PerformanceLog object.
        /// </summary>
        /// <param name="message">The body of the custom string.</param>
        /// <returns>Returns the same instance of PerformanceLog but with the added custom string property.</returns>
        public IPerformanceLog AddLogVariable(string message)
        {
            ExtraVariables.Add(message);
            return this;
        }
    }
}
