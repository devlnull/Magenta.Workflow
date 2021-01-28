using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowIdentity : FlowEntity
    {
        public FlowState FlowState { get; set; }
        public long FlowStateId { get; set; }
        public FlowIdentityTypes IdentityType { get; set; }
        public string IdentityId { get; set; }
    }
}
