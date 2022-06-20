using System;

namespace Magenta.Workflow.Core.Logger
{
    public class FlowConsoleLogger : IFlowLogger
    {
        public void LogError(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void LogInfo(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}
