namespace Magenta.Workflow.Core.Tasks
{
    public class FlowError
    {
        public FlowError(string message)
        {
            Message = message;
        }

        public FlowError(string message, string description) : this(message)
        {
            Description = description;
        }

        public FlowError(string message, string description, string code)
            : this(message, description)
        {
            Code = code;
        }

        public string Message { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
