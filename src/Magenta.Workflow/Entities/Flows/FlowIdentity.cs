using Magenta.Workflow.Entities.Base;
using Magenta.Workflow.Structures;

namespace Magenta.Workflow.Entities.Flows
{
    public class FlowIdentity : FlowEntity
    {
        public FlowState FlowState { get; set; }
        public long FlowStateId { get; set; }
        public FlowIdentityTypes IdentityType { get; set; }
        public string IdentityId { get; set; }
    }
}
