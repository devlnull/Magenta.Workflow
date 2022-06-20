using System;
using System.Collections.Generic;

namespace Magenta.Workflow.Builder
{
    public class FlowTransitionBuilder
    {
        internal List<FlowTransitionBuilderModel> FlowTransitionBuilderModels { get; set; } = new();
        internal FlowStateBuilderModel Parent { get; set; }
        //TODO: Add Identity models to set identity of transitions

        internal FlowTransitionBuilder AddFlowTransition(
            FlowTransitionBuilderModel flowTransitionBuilderModel, FlowStateBuilderModel parent)
        {
            Parent = parent;
            flowTransitionBuilderModel.SourceName = Parent?.Name;
            FlowTransitionBuilderModels.Add(flowTransitionBuilderModel);
            return this;
        }
    }
}
