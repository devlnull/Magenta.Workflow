using System;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.UseCases.InitFlowState
{
    public class InitFlowStateModel
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public FlowStateTypes StateType { get; set; }
    }
}
