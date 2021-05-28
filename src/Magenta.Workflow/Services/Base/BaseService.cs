using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Managers.States;
using System.Threading.Tasks;

namespace Magenta.Workflow.Services.Base
{
    public abstract class BaseService<TRequest,TResult>
        where TRequest : class
        where TResult : class
    {
        internal readonly IStateManager _stateManager;
        public BaseService(IStateManager stateManager)
        {
            _stateManager = stateManager ?? throw new FlowException(nameof(IStateManager));
        }

        public abstract Task<TResult> HandleRequestAsync(TRequest request);
    }
}
