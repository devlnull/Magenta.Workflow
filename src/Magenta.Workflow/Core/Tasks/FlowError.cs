namespace Magenta.Workflow.Core.Tasks
{
    public class FlowError
    {
        public FlowError(string message)
        {
            Message = message;
        }

        public FlowError(string message, params string[] args) : this(string.Format(message, args)) { }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetCode(string code)
        {
            Code = code;
        }


        public string Message { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
