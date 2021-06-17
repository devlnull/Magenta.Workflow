using System;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.UseCases.InitFlowState;

namespace Magenta.Workflow.Builder
{
    public class FlowStateBuilderModel
    {
        public FlowStateBuilderModel() { }

        public FlowStateBuilderModel(string name, string title,
            string tag, FlowStateTypes stateType)
        {
            Name = name;
            Title = title;
            Tag = tag;
            StateType = stateType;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public FlowStateTypes StateType { get; set; }

        public InitFlowStateModel MapToInit(Guid flowTypeId)
        {
            return new InitFlowStateModel()
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
