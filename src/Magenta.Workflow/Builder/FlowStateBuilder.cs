using System.Collections.Generic;

namespace Magenta.Workflow.Builder
{
    public class FlowStateBuilder
    {
        internal FlowTransitionBuilder FlowTransitionBuilder { get; set; } = new FlowTransitionBuilder();
        internal FlowStateBuilderModel _lastStateModel = null;
        internal List<FlowStateBuilderModel> FlowStateBuilderModels { get; set; } =
            new List<FlowStateBuilderModel>();
        //TODO: Add Identity models to set identity of states

        internal FlowStateBuilder AddFlowState(FlowStateBuilderModel flowStateBuilderModel)
        {
            _lastStateModel = flowStateBuilderModel;
            FlowStateBuilderModels.Add(flowStateBuilderModel);
            return this;
        }

        public FlowStateBuilder AddFlowTransition(
            FlowTransitionBuilderModel flowTransitionBuilderModel)
        {
            FlowTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, _lastStateModel);
            return this;
        }
    }
}
