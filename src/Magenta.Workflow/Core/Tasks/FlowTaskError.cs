namespace Sepid.Domain.Core.Tasks
{
    public class FlowTaskError
    {
        public FlowTaskError() { }
        public FlowTaskError(string message)
        {
            Message = message;
        }

        public FlowTaskError(string message, string description) : this(message)
        {
            Description = description;
        }

        public FlowTaskError(string message, string description, string code)
            : this(message, description)
        {
            Code = code;
        }

        public string Message { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
