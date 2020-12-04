using Magenta.Workflow.DataTransferObjects.Base;
using Magenta.Workflow.Structures;

namespace Magenta.Workflow.DataTransferObjects.Flows
{
    public class FlowIdentity : FlowEntity
    {
        public FlowState FlowState { get; set; }
        public long FlowStateId { get; set; }
        public FlowIdentityTypes IdentityType { get; set; }
        public string IdentityId { get; set; }
    }
}
