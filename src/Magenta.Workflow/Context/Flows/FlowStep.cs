using Magenta.Workflow.Context.Base;
using System;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowStep : FlowEntity
    {
        public FlowInstance Instance { get; set; }
        public Guid InstanceId { get; set; }
        public FlowTransition Transition { get; set; }
        public string Comment { get; set; }
        public string Payload { get; set; }
        public Guid TransitionId { get; set; }
        public string IdentityId { get; set; }
        public bool IsCurrent { get; set; }
    }
}
