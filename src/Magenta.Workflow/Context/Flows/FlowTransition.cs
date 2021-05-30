using System;
using System.Collections.Generic;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowTransition : FlowEntity
    {
        public FlowTransition()
        {
            Steps = new HashSet<FlowStep>();
            Reasons = new HashSet<FlowTransitionReason>();
        }

        public string Title { get; set; }
        public string Name { get; set; }
        public FlowState Source { get; set; }
        public FlowTransitionTypes TransitionType { get; set; }
        public bool IsAutomatic { get; set; }
        public Guid? SourceId { get; set; }
        public FlowState Destination { get; set; }
        public Guid DestinationId { get; set; }
        public FlowType FlowType { get; set; }
        public Guid TypeId { get; set; }
        public ICollection<FlowStep> Steps { get; set; }
        public ICollection<FlowTransitionReason> Reasons { get; set; }
    }
}