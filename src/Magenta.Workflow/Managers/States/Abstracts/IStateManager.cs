using Magenta.Workflow.Entities.Base;

namespace Magenta.Workflow.Managers.States.Abstracts
{
    public interface IStateManager
    {
        IFlowSet<TEntity> GetFlowSet<TEntity>() where TEntity : FlowEntity;
    }
}
