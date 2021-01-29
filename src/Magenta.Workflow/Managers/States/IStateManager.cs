using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Managers.States
{
    public interface IStateManager
    {
        IFlowSet<TEntity> GetFlowSet<TEntity>() where TEntity : FlowEntity;
    }
}
