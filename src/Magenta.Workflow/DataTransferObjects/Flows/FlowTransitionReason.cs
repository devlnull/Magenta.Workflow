using Magenta.Workflow.DataTransferObjects.Base;

namespace Magenta.Workflow.DataTransferObjects.Flows
{
    public class FlowTransitionReason : FlowEntity
    {
        public string Description { get; set; }
        public long FlowTransitionId { get; set; }
        public FlowTransition FlowTransition { get; set; }
    }
}