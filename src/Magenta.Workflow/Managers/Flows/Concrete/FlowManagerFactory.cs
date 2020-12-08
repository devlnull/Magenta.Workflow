using Magenta.Workflow.Core.Patterns;

namespace Magenta.Workflow
{
    public class FlowManagerFactory : ManagerFactory<IFlowManager>
    {
        public override IFlowManager CreateInstance()
        {
            return new FlowManager();
        }
    }
}
