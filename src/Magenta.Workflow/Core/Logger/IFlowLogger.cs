namespace Magenta.Workflow.Core.Logger
{
    public interface IFlowLogger
    {
        public void LogError(string message, params string[] args);
        public void LogInfo(string message, params string[] args);
        public void LogWarning(string message, params string[] args);
    }
}
