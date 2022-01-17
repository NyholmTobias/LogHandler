using LogHandler.Implementations;

namespace LogHandler.Interfaces
{
    public interface IDebugLog : ILogMessage
    {
        IDebugLog AddLogVariable(string message);
        static DebugLog CreateDebugLog(string methodName, string messageText = "")
        {
            throw new NotImplementedException();
        }
    }
}
