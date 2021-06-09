using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Managers.States;

namespace Magenta.Workflow.Services.Base
{
    public class BaseService
    {
        internal readonly IStateManager StateManager;
        public BaseService(IStateManager stateManager)
        {
            StateManager = stateManager ?? throw new FlowException(nameof(IStateManager));
        }

    }
}
