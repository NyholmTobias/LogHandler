using LogHandler.Implementations;
using LogHandler.Interfaces;
using System.Diagnostics;

namespace LogHandler
{
    public class LogHandler
    {
        internal List<ILogMessage> messages = new();
        private static readonly LogHandler _instance = new(); 
        private LogHandler()
        {
        }
        public static LogHandler GetInstance()
        {
            return _instance;
        }
        public virtual void AddLogToBulk(ILogMessage log)
        {
            if (log is null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            messages.Add(log);
        }

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
                if(messages.Contains(log)) messages.Remove(log);
                return true;
            }

            if (log.GetType().Equals(typeof(DebugLog)))
            {
                DebugLog dLog = (DebugLog)log;
                Debug.Write(DebugLogToString(dLog));
                if (messages.Contains(log)) messages.Remove(log);
                return true;
            }

            throw new ArgumentException(null, nameof(log));
        }

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
                    $"Time taken in milliseconds: Unable too log time taken.\n" +
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
                    $"Time taken in milliseconds: Unable too log time taken.";
            }

            if (perfLog.ExtraVariables.Count != 0)
            {
                var extraVariablesString = string.Join("\nExtra variable: ",
                    perfLog.ExtraVariables.Select(ev => ev).ToArray());
                stringLog += extraVariablesString;
            }


            return stringLog;
        }

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