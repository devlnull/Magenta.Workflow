using System.Collections.Generic;
using Magenta.Workflow.Entities.Base;
using Magenta.Workflow.Structures;

namespace Magenta.Workflow.Entities.Flows
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
        public long? SourceId { get; set; }
        public FlowState Destination { get; set; }
        public long DestinationId { get; set; }
        public ICollection<FlowStep> Steps { get; set; }
        public ICollection<FlowTransitionReason> Reasons { get; set; }
    }
}