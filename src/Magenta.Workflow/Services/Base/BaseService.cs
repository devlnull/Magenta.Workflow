using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Managers.States;

namespace Magenta.Workflow.Services.Base
{
    public class BaseService
    {
        internal readonly IStateManager _stateManager;
        public BaseService(IStateManager stateManager)
        {
            _stateManager = stateManager ?? throw new FlowException(nameof(IStateManager));
        }

    }
}
