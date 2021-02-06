using System;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.UseCases.InitFlowTransition
{
    public class InitFlowTransitionModel
    {
        public Guid SourceGuidRow { get; set; }
        public Guid DestinationGuidRow { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public FlowTransitionTypes TransitionType { get; set; }
    }
}
