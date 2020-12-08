using Magenta.Workflow.Entities.Base;

namespace Magenta.Workflow.Entities.Flows
{
    public class FlowTransitionReason : FlowEntity
    {
        public string Description { get; set; }
        public long FlowTransitionId { get; set; }
        public FlowTransition FlowTransition { get; set; }
    }
}