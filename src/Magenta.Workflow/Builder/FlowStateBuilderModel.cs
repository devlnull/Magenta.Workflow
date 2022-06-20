using System;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.UseCases.InitFlowState;

namespace Magenta.Workflow.Builder
{
    public class FlowStateBuilderModel
    {
        public FlowStateBuilderModel(string name, string title, FlowStateTypes stateType)
        {
            Name = name;
            Title = title;
            StateType = stateType;
        }

        public string Name { get; set; }
        private string Title { get; set; }
        private string Tag { get; set; }
        private FlowStateTypes StateType { get; set; }

        public InitFlowStateRequest MapToInit(Guid flowTypeId)
        {
            return new InitFlowStateRequest()
            {
                Name = Name,
                StateType = StateType,
                Tag = Tag,
                Title = Title,
                TypeId = flowTypeId
            };
        }
    }
}
