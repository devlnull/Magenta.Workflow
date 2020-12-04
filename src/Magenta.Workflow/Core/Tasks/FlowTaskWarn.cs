namespace Magenta.Workflow.Core.Tasks
{
    public class FlowTaskWarn
    {
        public FlowTaskWarn() { }
        public FlowTaskWarn(string message)
        {
            Message = message;
        }

        public FlowTaskWarn(string message, string description) : this(message)
        {
            Description = description;
        }

        public FlowTaskWarn(string message, string description, string code)
            : this(message, description)
        {
            Code = code;
        }

        public string Message { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
