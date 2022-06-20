namespace Magenta.Workflow.Core.Logger
{
    public interface IFlowLogger
    {
        public void LogError(string message, params object[] args);
        public void LogInfo(string message, params object[] args);
        public void LogWarning(string message, params object[] args);
    }
}
