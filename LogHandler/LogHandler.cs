using LogHandler.Implementations;
using LogHandler.Interfaces;
using System.Diagnostics;

namespace LogHandler
{
    public class LogHandler
    {
        internal List<ILogMessage> messages = new();
        private static readonly LogHandler _instance = new(); 
        /// <summary>
        /// Private constructor because of singleton pattern, use GetInstance.
        /// </summary>
        private LogHandler()
        {
        }
        /// <summary>
        /// Gets an instance of LogHandler.
        /// </summary>
        /// <returns>Singleton instance of LogHandler.</returns>
        public static LogHandler GetInstance()
        {
            return _instance;
        }
        /// <summary>
        /// Saves instance of ILogMessage too bulk list.
        /// </summary>
        /// <param name="log">ILogMessage too be added to bulk list.</param>
        /// <returns>Returns true if successfully added, otherwise exeption.</returns>
        /// <exception cref="ArgumentNullException">Log is null.</exception>
        /// <exception cref="ArgumentException">Log is of wrong type.</exception>
        public virtual bool AddLogToBulk(ILogMessage log)
        {
            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            try
            {
                messages.Add(log);
                return true;
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Prints all instances of ILogMessage from bulk list in debug window, then clears the list.
        /// </summary>
        /// <returns>Returns true if successfully printed, otherwise false.</returns>
        public virtual bool PrintLogBulk()
        {
            try
            {
                messages.ForEach(log => PrintLog(log));
                messages.Clear();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exeption caught: {0}", ex);
                return false;
            }
        }

        /// <summary>
        /// Prints instance of ILogMessage in debug window.
        /// </summary>
        /// <param name="log">ILogMessage to be printed.</param>
        /// <returns>Returns true if printed successfully, otherwise exeption.</returns>
        /// <exception cref="ArgumentNullException">Log is null.</exception>
        /// <exception cref="ArgumentException">Log is of wrong type.</exception>
        public virtual bool PrintLog(ILogMessage log)
        {
            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }
            
            if(log.GetType().Equals(typeof(PerformanceLog)) || log is PerformanceLog)
            {
                PerformanceLog pLog = (PerformanceLog)log;
                Debug.Write(PerformanceLogToString(pLog));
                return true;
            }

            if (log.GetType().Equals(typeof(DebugLog)))
            {
                DebugLog dLog = (DebugLog)log;
                Debug.Write(DebugLogToString(dLog));
                return true;
            }

            throw new ArgumentException(null, nameof(log));
        }

        /// <summary>
        /// Deserializes PerformanceLog or PerformanceLog<T> into string. 
        /// </summary>
        /// <param name="perfLog">PerformanceLog to be deserialized.</param>
        /// <returns>String that can be printed.</returns>
        private static string PerformanceLogToString(PerformanceLog perfLog)
        {
            string stringLog = "";

            if (perfLog.Milliseconds != 0 &&
                !string.IsNullOrWhiteSpace(perfLog.MessageText))
            {
                stringLog = $"Method: {perfLog.MethodName}\n" +
                    $"Time taken in milliseconds: {perfLog.Milliseconds}\n" +
                    $"Message: {perfLog.MessageText}";
            }

            else if (perfLog.Milliseconds == 0 &&
                !string.IsNullOrWhiteSpace(perfLog.MessageText))
            {
                stringLog = $"Method: {perfLog.MethodName}\n" +
                    $"Time taken in milliseconds: Less than 0 milliseconds.\n" +
                    $"Message: {perfLog.MessageText}";
            }

            else if (perfLog.Milliseconds != 0 &&
                string.IsNullOrWhiteSpace(perfLog.MessageText))
            {
                stringLog = $"Method: {perfLog.MethodName}\n" +
                    $"Time taken in milliseconds: {perfLog.Milliseconds}";
            }

            else if (perfLog.Milliseconds == 0 &&
                string.IsNullOrWhiteSpace(perfLog.MessageText))
            {
                stringLog = $"Method: {perfLog.MethodName}\n" +
                    $"Time taken in milliseconds: Less than 0 milliseconds.";
            }

            if (perfLog.ExtraVariables.Count != 0)
            {
                var extraVariablesString = "\nExtra variable: " 
                    + string.Join("\nExtra variable: ",
                    perfLog.ExtraVariables.Select(ev => ev).ToArray());
                stringLog += extraVariablesString;
            }

            return stringLog;
        }

        /// <summary>
        /// Deserializes DebugLog into string. 
        /// </summary>
        /// <param name="debLog">DebugLog to be deserialized.</param>
        /// <returns>String that can be printed.</returns>
        private static string DebugLogToString(DebugLog debLog)
        {
            string stringLog = "";

            if (!string.IsNullOrWhiteSpace(debLog.MessageText))
            {
                stringLog = $"Method: {debLog.MethodName}\n" +
                    $"Message: {debLog.MessageText}";
            }
            else
            {
                stringLog = $"Method: {debLog.MethodName}\n";
            }
            if (debLog.ExtraVariables.Count != 0)
            {
                var extraVariablesString = string.Join("\nExtra variable: ",
                    debLog.ExtraVariables.Select(ev => ev).ToArray());
                stringLog += extraVariablesString;
            }

            return stringLog;
        }
    }
}