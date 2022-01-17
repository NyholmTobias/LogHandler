using LogHandler.Implementations;

namespace LogHandler.Interfaces
{
    public interface IPerformanceLog : ILogMessage
    {
        static PerformanceLog CreatePerformanceLog(string methodName,
                                                    Action function,
                                                    string messageText = "")
        {
            throw new NotImplementedException();
        }
        static PerformanceLog CreatePerformanceLog<T>(string methodName,
                                                                 Func<T> function,
                                                                 string messageText = "")
        {
            throw new NotImplementedException();
        }
        IPerformanceLog AddLogVariable(string message);
    }
}
