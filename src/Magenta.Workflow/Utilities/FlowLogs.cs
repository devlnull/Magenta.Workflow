namespace Magenta.Workflow.Utilities
{
    internal class FlowLogs
    {
        public const string STATE_NOTFOUND = "state with id '{0}' not found.";
        public const string TRANISTION_NOTFOUND = "transition with id '{0}' not found.";
        public const string INSTANCE_NOTFOUND = "instance with id '{0}' not found.";
        public const string STEP_NOTFOUND = "step with id '{0}' not found.";
        public const string FLOWTYPE_NOTFOUND = "flow type '{0}' not found.";
        public const string REQUEST_STARTED = "[request of type '{0}' ftarted]";
        public const string REQUEST_FINISHED = "[request of type '{0}' finished]";
        public const string REQUEST_HAS_ERROR = "[request has '{0}' errors]";
        public const string REQUEST_HAS_WARN = "[request has '{0}' warnings]";
        public const string REQUEST_OPERATION_STARTED = "[request operation of type '{0}' started]";
        public const string REQUEST_OPERATION_FINISHED = "[request operation of type '{0}' finished]";
        public const string EXCEPTION_OCCURED = "[an exception occured] [details: {0}]";

    }
}
