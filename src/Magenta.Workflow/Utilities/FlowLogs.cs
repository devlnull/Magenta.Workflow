namespace Magenta.Workflow.Utilities
{
    internal class FlowLogs
    {
        public const string StateNotfound = "state with id '{0}' not found.";
        public const string TranistionNotfound = "transition with id '{0}' not found.";
        public const string InstanceNotfound = "instance with id '{0}' not found.";
        public const string StepNotfound = "step with id '{0}' not found.";
        public const string FlowtypeNotfound = "flow type '{0}' not found.";
        public const string RequestStarted = "[request of type '{0}' ftarted]";
        public const string RequestFinished = "[request of type '{0}' finished]";
        public const string RequestHasError = "[request has '{0}' errors]";
        public const string RequestHasWarn = "[request has '{0}' warnings]";
        public const string RequestOperationStarted = "[request operation of type '{0}' started]";
        public const string RequestOperationFinished = "[request operation of type '{0}' finished]";
        public const string ExceptionOccured = "[an exception occured] [details: {0}]";

    }
}
