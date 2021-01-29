namespace Magenta.Workflow.Core.Tasks
{
    public class FlowWarn
    {
        public FlowWarn(string message)
        {
            Message = message;
        }

        public FlowWarn(string message, params string[] args) : this(string.Format(message, args)) { }


        public FlowWarn(string message, string description) : this(message)
        {
            Description = description;
        }

        public FlowWarn(string message, string description, string code)
            : this(message, description)
        {
            Code = code;
        }

        public string Message { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
