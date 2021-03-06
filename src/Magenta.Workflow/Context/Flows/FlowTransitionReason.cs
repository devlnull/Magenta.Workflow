using Magenta.Workflow.Context.Base;
using System;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowTransitionReason : FlowEntity
    {
        public string Description { get; set; }
        public Guid TransitionId { get; set; }
        public FlowTransition Transition { get; set; }
    }
}