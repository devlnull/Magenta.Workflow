using System;

namespace Magenta.Workflow.Core.Exceptions
{
    public class FlowException : Exception
    {
        public FlowException(string message) : base(message)
        {
        }

        public FlowException(string message, params string[] args):base(string.Format(message, args))
        {
        }

        public FlowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
