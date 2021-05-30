using Magenta.Workflow.Core.Logger;
using Magenta.Workflow.Managers.Flows;
using Magenta.Workflow.Tests.Mock;

namespace Magenta.Workflow.Tests.Infrastructures
{
    public static class ManagerFactory
    {
        public static IFlowManager GetFlowManager()
        {
            return new FlowManager(MockState.MockStateManager(), new FlowConsoleLogger());
        }

        public static IFlowReportManager GetFlowReportManager()
        {
            return new FlowReportManager(MockState.MockStateManager());
        }
    }
}
