using LogHandler.Interfaces;

namespace LogHandler.Implementations
{
    public class DebugLog : IDebugLog
    {
        public readonly string MethodName = "";
        public readonly string MessageText = "";
        public readonly List<string> ExtraVariables = new();

        private DebugLog(string methodName, string messageText = "")
        {
            MethodName = methodName;
            MessageText = messageText;
        }

        public static DebugLog CreateDebugLog(string methodName, string messageText = "")
        {
            return new DebugLog(methodName, messageText);
        }

        public IDebugLog AddLogVariable(string message)
        {   
            ExtraVariables.Add(message);
            return this;
        }
    }
}
