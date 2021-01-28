using Magenta.Workflow.Managers.Flows;

namespace Magenta.Workflow.Tests.Infrastructures
{
    public static class ManagerFactory
    {
        public static IFlowManager GetFlowManager()
        {
            var stateManager = Mock.MockState.MockStateManager();
            return new FlowManager(stateManager);
        }
    }
}
