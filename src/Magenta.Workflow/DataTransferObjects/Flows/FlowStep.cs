using Magenta.Workflow.DataTransferObjects.Base;

namespace Magenta.Workflow.DataTransferObjects.Flows
{
    public class FlowStep : FlowEntity
    {
        public FlowInstance Instance { get; set; }
        public long InstanceId { get; set; }
        public FlowTransition Transition { get; set; }
        public string Comment { get; set; }
        public string Payload { get; set; }
        public long TransitionId { get; set; }
        public string IdentityId { get; set; }
        public bool IsCurrent { get; set; }
    }
}
