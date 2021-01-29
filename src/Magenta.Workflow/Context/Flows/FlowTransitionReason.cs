using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowTransitionReason : FlowEntity
    {
        public string Description { get; set; }
        public long FlowTransitionId { get; set; }
        public FlowTransition FlowTransition { get; set; }
    }
}