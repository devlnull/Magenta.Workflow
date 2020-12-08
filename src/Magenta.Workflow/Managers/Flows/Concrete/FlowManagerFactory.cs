using Magenta.Workflow.Core.Patterns;

namespace Magenta.Workflow
{
    public class FlowManagerFactory : IManagerFactory<IFlowManager>
    {
        public IFlowManager CreateInstance()
        {
            return new FlowManager();
        }
    }
}
