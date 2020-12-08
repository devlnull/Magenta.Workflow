namespace Magenta.Workflow.Core.Patterns
{
    public abstract class ManagerFactory<T>
    {
        public abstract T CreateInstance();
    }
}
