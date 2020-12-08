using Magenta.Workflow.Core.Patterns;
using Magenta.Workflow.Managers.States.Abstracts;

namespace Magenta.Workflow.Managers.States.Concrete
{
    public class StateManagerFactory : IManagerFactory<IStateManager>
    {
        public IStateManager CreateInstance()
        {
            return new InMemoryStateManager();
        }
    }
}
