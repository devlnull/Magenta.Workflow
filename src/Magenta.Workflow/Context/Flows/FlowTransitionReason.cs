using Magenta.Workflow.Context.Base;
using System;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowTransitionReason : FlowEntity
    {
        public string Description { get; set; }
        public Guid FlowTransitionId { get; set; }
        public FlowTransition FlowTransition { get; set; }
    }
}