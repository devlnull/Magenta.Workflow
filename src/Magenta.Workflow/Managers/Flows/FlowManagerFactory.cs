using Magenta.Workflow.Core.Patterns;

namespace Magenta.Workflow.Managers.Flows
{
    public class FlowManagerFactory : ManagerFactory<IFlowManager>
    {
        public override IFlowManager CreateInstance()
        {
            return new FlowManager();
        }
    }
}
