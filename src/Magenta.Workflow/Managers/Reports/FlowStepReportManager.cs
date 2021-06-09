using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Managers.Reports
{
    public partial class FlowReportManager
    {

        public async Task<FlowResult<IEnumerable<FlowStep>>> GetInstanceStepsAsync(Guid instanceId)
        {
            //Get current instance
            if (instanceId.GuidIsEmpty())
                throw new ArgumentNullException(nameof(instanceId));

            var instanceSet = StateManager.GetFlowSet<FlowInstance>();

            var targetInstance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(instanceId));
            if (targetInstance == null)
                return FlowResult<IEnumerable<FlowStep>>
                    .Failed(new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));

            //Get all steps
            var stepSet = StateManager.GetFlowSet<FlowStep>();
            var steps = await stepSet.GetAllAsync(x => x.InstanceId.Equals(targetInstance.Id));

            return FlowResult<IEnumerable<FlowStep>>.Successful(steps);
        }

    }
}