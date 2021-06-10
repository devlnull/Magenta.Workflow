using Magenta.Workflow.Core.Logger;
using Magenta.Workflow.Managers.Flows;
using Magenta.Workflow.Managers.Reports;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Tests.Mock;

namespace Magenta.Workflow.Tests.Infrastructures
{
    public class ManagerFactory
    {
        public IFlowManager GetFlowManager(IStateManager stateManager)
        {
            return new FlowManager(stateManager, new FlowConsoleLogger());
        }

        public IFlowReportManager GetFlowReportManager(IStateManager stateManager)
        {
            return new FlowReportManager(stateManager, new FlowConsoleLogger());
        }
    }
}
