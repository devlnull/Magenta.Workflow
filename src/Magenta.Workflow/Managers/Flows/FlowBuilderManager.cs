using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenta.Workflow.Builder;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;

namespace Magenta.Workflow.Managers.Flows
{
    public partial class FlowManager
    {
        public async Task<FlowResult> BuildWorkflowAsync(
            WorkflowBuilder workflowBuilder)
        {
            var result = new FlowResult();
            var typeItem = workflowBuilder.WorkflowBuilderModel;
            var typeResult = await BuilderCreateFlowTypeAsync(typeItem);
            result.Merge(typeResult);
            if (result.Succeeded == false)
                return result;
            Guid flowTypeId = typeResult.Result.Id;

            var stateItems = workflowBuilder.FlowStateBuilder.FlowStateBuilderModels;
            var statesResult = await BuilderCreateFlowStateAsync(flowTypeId, stateItems);
            statesResult.ToList().ForEach(x => result.Merge(x));
            if (result.Succeeded == false)
                return result;

            var transitionItems = workflowBuilder.FlowStateBuilder
                .FlowTransitionBuilder.FlowTransitionBuilderModels;
            var transitionResult = await BuilderCreateFlowTransitionsAsync(flowTypeId, transitionItems);
            transitionResult.ToList().ForEach(x => result.Merge(x));
            if (result.Succeeded == false)
                return result;

            return result;
        }

        async Task<FlowResult<FlowType>> BuilderCreateFlowTypeAsync(
            WorkflowBuilderModel flowTypeBuilderModel)
        {
            var mappedObject = flowTypeBuilderModel.MapToInit();
            var typeResult = await InitFlowTypeAsync(mappedObject);
            return typeResult;
        }

        async Task<IEnumerable<FlowResult<FlowState>>> BuilderCreateFlowStateAsync(
            Guid flowTypeId, IEnumerable<FlowStateBuilderModel> flowStateBuilderModels)
        {
            var result = new List<FlowResult<FlowState>>();
            foreach (var model in flowStateBuilderModels)
            {
                var mappedObject = model.MapToInit(flowTypeId);
                var stateResult = await InitFlowStateAsync(mappedObject);
                result.Add(stateResult);
            }
            return result;
        }

        async Task<IEnumerable<FlowResult<FlowTransition>>> BuilderCreateFlowTransitionsAsync(
            Guid flowTypeId, IEnumerable<FlowTransitionBuilderModel> models)
        {
            var result = new List<FlowResult<FlowTransition>>();
            var set = StateManager.GetFlowSet<FlowState>();
            foreach (var model in models)
            {
                var mappedObject = await model.MapToInitAsync(set, flowTypeId);
                var transitionResult = await this.InitFlowTransitionAsync(mappedObject);
                result.Add(transitionResult);
            }

            return result;
        }
    }
}
