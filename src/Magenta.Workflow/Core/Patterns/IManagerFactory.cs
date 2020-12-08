namespace Magenta.Workflow.Core.Patterns
{
    public interface IManagerFactory<T>
    {
        T CreateInstance();
    }
}
