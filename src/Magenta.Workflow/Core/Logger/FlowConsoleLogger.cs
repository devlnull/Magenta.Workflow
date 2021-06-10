using System;

namespace Magenta.Workflow.Core.Logger
{
    public class FlowConsoleLogger : IFlowLogger
    {
        public void LogError(string message, params string[] args)
        {
            Console.WriteLine(message);
        }

        public void LogInfo(string message, params string[] args)
        {
            Console.WriteLine(message);
        }

        public void LogWarning(string message, params string[] args)
        {
            Console.WriteLine(message);
        }
    }
}
