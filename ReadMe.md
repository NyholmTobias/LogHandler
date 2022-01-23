# LogHandler

LogHandler is a class library that lets you log your project in a simple way. By default you can debug and performance log, and the log messages are displayed in the debug window. 
You may also bulk the messages so that they will be writen all at once. There is a function to add your own specific string properties. There is support for performance logging 
methods that both has and dont have return values. Most major functionality is virtual so that you can customize the messages, enviroment and write options.

## Installation

Use the package manager in Visual Studio to install LogHandler or use the package-manager window.
```
install-package LogHandler
```

## Usage

Get a singleton instance of LogHandler by calling LogHandler.GetInstance();. Then eighter call PrintLog or AddLogToBulk + PrintLogBulk and pass the required parameters.

```cs
using LogHandler;

namespace LogHandlerTest
{
    public class LogHandlerTest
    {
        private readonly LogHandler _loghandler = LogHandler.GetInstance();

        public void ThingsHappen()
        {
            _loghandler.AddLogToBulk(DebugLog.CreateDebugLog("Test", "Test"));
            _loghandler.AddLogToBulk(DebugLog.CreateDebugLog("Test", "Test"));
            _loghandler.AddLogToBulk(DebugLog.CreateDebugLog("Test", "Test"));
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodWithIntReturnValueForPerformanceTests(10), "Test").log);
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodWithIntReturnValueForPerformanceTests(10), "Test").log);
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodWithIntReturnValueForPerformanceTests(10), "Test").log);
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodWithStringReturnValueForPerformanceTests(10), "Test").log);
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodWithStringReturnValueForPerformanceTests(10), "Test").log);
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodWithStringReturnValueForPerformanceTests(10), "Test").log);

            
            _loghandler.PrintLogBulk();
        }
    }


```

## Source code
You can find the code on my [Github](https://github.com/NyholmTobias/LogHandler) account.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
