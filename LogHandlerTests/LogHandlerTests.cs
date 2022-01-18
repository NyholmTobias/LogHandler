using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogHandler.Implementations;
using System.Diagnostics;

namespace LogHandler.Tests
{
    [TestClass]
    public class LogHandlerTests
    {
        private readonly LogHandler _loghandler = LogHandler.GetInstance();

        [TestMethod()]
        public void AddDebugLogToBulkTest()
        {
            //Arrange 
            var log = DebugLog.CreateDebugLog("Test", "Test");

            //Act
            var res = _loghandler.AddLogToBulk(log);


            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void AddPerformanceLogToBulkTest()
        {
            //Arrange 
            var log = PerformanceLog.CreatePerformanceLog("Test",
                () => MethodForPerformanceTests(40),
                "Test");

            //Act
            var res = _loghandler.AddLogToBulk(log);


            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void AddGenericPerformanceLogToBulkTest()
        {
            //Arrange 
            var log = PerformanceLog<int>.CreatePerformanceLog("Test",
                () => MethodWithIntReturnValueForPerformanceTests(3),
                "Test");

            //Act
            var res = _loghandler.AddLogToBulk(log);


            //Assert
            Assert.IsTrue(res);
            Assert.AreEqual(8, log.ReturnValue);
        }

        [TestMethod()]
        public void PrintLogBulkTest()
        {
            //Arrange
            _loghandler.AddLogToBulk(DebugLog.CreateDebugLog("Test", "Test"));
            _loghandler.AddLogToBulk(DebugLog.CreateDebugLog("Test", "Test"));
            _loghandler.AddLogToBulk(DebugLog.CreateDebugLog("Test", "Test"));
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog.CreatePerformanceLog("Test", () => MethodForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog<int>.CreatePerformanceLog("Test", () => MethodWithIntReturnValueForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog<int>.CreatePerformanceLog("Test", () => MethodWithIntReturnValueForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog<int>.CreatePerformanceLog("Test", () => MethodWithIntReturnValueForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog<string>.CreatePerformanceLog("Test", () => MethodWithStringReturnValueForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog<string>.CreatePerformanceLog("Test", () => MethodWithStringReturnValueForPerformanceTests(10), "Test"));
            _loghandler.AddLogToBulk(PerformanceLog<string>.CreatePerformanceLog("Test", () => MethodWithStringReturnValueForPerformanceTests(10), "Test"));

            //Act
            var res = _loghandler.PrintLogBulk();

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintDebugLog_AllStandardVariables_SuccessfulResult()
        {
            //Arrange 
            var debugLog = DebugLog.CreateDebugLog("TestMethod", 
                "Hello");

            //Act
            var res = _loghandler.PrintLog(debugLog);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintDebugLog_NoMessage_SuccessfulResult()
        {
            //Arrange 
            var debugLog = DebugLog.CreateDebugLog("TestMethod");

            //Act
            var res = _loghandler.PrintLog(debugLog);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintDebugLog_WithExtraVariable_SuccessfulResult()
        {
            //Arrange 
            var debugLog = DebugLog.CreateDebugLog("TestMethod",
                "Hello");
            debugLog.AddLogVariable("Extra Cheese please");

            //Act
            var res = _loghandler.PrintLog(debugLog);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintPerformanceLog_AllStandardVariables_SuccessfulResult()
        {
            //Arrange 
            var perfLog = PerformanceLog.CreatePerformanceLog("TestMethod",
                () => MethodForPerformanceTests(50), 
                "Hello");
            
            //Act
            var res = _loghandler.PrintLog(perfLog);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintPerformanceLog_NoMessage_SuccessfulResult()
        {
            //Arrange 
            var perfLog = PerformanceLog.CreatePerformanceLog("TestMethod",
                () => MethodForPerformanceTests(50));

            //Act
            var res = _loghandler.PrintLog(perfLog);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintPerformanceLog_WithExtraVariable_SuccessfulResult()
        {
            //Arrange 
            var perfLog = PerformanceLog.CreatePerformanceLog("TestMethod",
                () => MethodForPerformanceTests(50),
                "Hello");
            perfLog.AddLogVariable("Extra cheese please");

            //Act
            var res = _loghandler.PrintLog(perfLog);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintPerformanceLog_ShortFunction_SuccessfulResult()
        {
            //Arrange 
            var lh = LogHandler.GetInstance();
            var perfLog = PerformanceLog.CreatePerformanceLog("TestMethod",
                () => Debug.Write("Test"),
                "Hello");

            //Act
            var res = _loghandler.PrintLog(perfLog);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void PrintPerformanceLog_AllStandardVariables_SuccessfulResultAndAStringReturnValue()
        {
            //Arrange 
            var perfLog = PerformanceLog<string>.CreatePerformanceLog("TestMethod",
                () => MethodWithStringReturnValueForPerformanceTests(50),
                "Hello");

            //Act
            var res = _loghandler.PrintLog(perfLog);

            //Assert
            Assert.IsTrue(res);
            Assert.AreEqual("hello, you made it!", perfLog.ReturnValue);
        }

        [TestMethod()]
        public void PrintPerformanceLog_AllStandardVariables_SuccessfulResultAndAnIntReturnValue()
        {
            //Arrange 
            var perfLog = PerformanceLog<int>.CreatePerformanceLog("TestMethod",
                () => MethodWithIntReturnValueForPerformanceTests(3),
                "Hello");

            //Act
            var res = _loghandler.PrintLog(perfLog);

            //Assert
            Assert.IsTrue(res);
            Assert.AreEqual(8, perfLog.ReturnValue);
        }

        private static void MethodForPerformanceTests(int numberOfRounds)
        {
            var a = 1;
            for (int i = 0; i < numberOfRounds; i++)
            {
                a += a;
            }
        }

        private static string MethodWithStringReturnValueForPerformanceTests(int numberOfRounds)
        {
            var a = 1;
            for (int i = 0; i < numberOfRounds; i++)
            {
                a += a;
            }
            return "hello, you made it!";
        }

        private static int MethodWithIntReturnValueForPerformanceTests(int numberOfRounds)
        {
            var a = 1;
            for (int i = 0; i < numberOfRounds; i++)
            {
                a += a;
            }
            return a;
        }
    }

}