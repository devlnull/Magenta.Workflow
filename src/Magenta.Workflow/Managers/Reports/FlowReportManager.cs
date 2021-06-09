using System;
using Magenta.Workflow.Core.Logger;
using Magenta.Workflow.Managers.States;

namespace Magenta.Workflow.Managers.Reports
{
    public partial class FlowReportManager : IFlowReportManager
    {
        public FlowReportManager(IStateManager stateManager,
            IFlowLogger logger)
        {
            StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IStateManager StateManager { get; set; }
        public IFlowLogger Logger { get; set; }
    }
}
