using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Structures;
using System;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowIdentity : FlowEntity
    {
        public FlowState FlowState { get; set; }
        public Guid FlowStateId { get; set; }
        public FlowIdentityTypes IdentityType { get; set; }
        public string IdentityId { get; set; }
    }
}
