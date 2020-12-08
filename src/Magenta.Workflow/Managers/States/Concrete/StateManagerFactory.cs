using Magenta.Workflow.Core.Patterns;
using Magenta.Workflow.Managers.States.Abstracts;

namespace Magenta.Workflow.Managers.States.Concrete
{
    public class StateManagerFactory : ManagerFactory<IStateManager>
    {
        private static InMemoryStateManager instance;
        public override IStateManager CreateInstance()
        {
            if (instance == null)
                instance = new InMemoryStateManager();
            return instance;
        }
    }
}
