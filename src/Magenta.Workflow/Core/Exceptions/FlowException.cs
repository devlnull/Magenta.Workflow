using System;

namespace Magenta.Workflow.Core.Exceptions
{
    public class FlowException : Exception
    {
        public FlowException()
        {
        }

        public FlowException(string message) : base(message)
        {
        }

        public FlowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
