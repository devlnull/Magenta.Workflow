using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Structures;
using System;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowIdentity : FlowEntity
    {
        public FlowState State { get; set; }
        public Guid StateId { get; set; }
        public FlowIdentityTypes IdentityType { get; set; }
        public string IdentityId { get; set; }
    }
}
