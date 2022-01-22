using LogHandler.Interfaces;

namespace LogHandler.Implementations
{
    public class DebugLog : IDebugLog
    {
        public readonly string MethodName = "";
        public readonly string MessageText = "";
        public readonly List<string> ExtraVariables = new();

        /// <summary>
        /// Private constructor, call CreateDebugLog instead.
        /// </summary>
        /// <param name="methodName">The name of the method that is beeing debugged.</param>
        /// <param name="messageText">Specific message.</param>
        private DebugLog(string methodName, string messageText = "")
        {
            MethodName = methodName;
            MessageText = messageText;
        }

        /// <summary>
        /// Creates an instance of DebugLog.
        /// </summary>
        /// <param name="methodName">The name of the method that is beeing debugged.</param>
        /// <param name="messageText">Specific message.</param>
        /// <returns>New DebugLog instance.</returns>
        public static DebugLog CreateDebugLog(string methodName, string messageText = "")
        {
            return new DebugLog(methodName, messageText);
        }

        /// <summary>
        /// Adds a custom string property to the DebugLog object.
        /// </summary>
        /// <param name="message">The body of the custom string.</param>
        /// <returns>Returns the same instance of DebugLog but with the added custom string property.</returns>
        public IDebugLog AddLogVariable(string message)
        {   
            ExtraVariables.Add(message);
            return this;
        }
    }
}
