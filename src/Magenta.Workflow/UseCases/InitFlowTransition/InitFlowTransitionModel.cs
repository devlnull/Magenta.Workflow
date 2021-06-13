using System;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.UseCases.InitFlowTransition
{
    public class InitFlowTransitionModel
    {
        public Guid SourceId { get; set; }
        public Guid DestinationId { get; set; }
        public Guid TypeId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public FlowTransitionTypes TransitionType { get; set; }
    }
}
