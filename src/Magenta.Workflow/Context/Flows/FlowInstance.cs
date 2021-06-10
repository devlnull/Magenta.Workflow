using System;
using System.Collections.Generic;
using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowInstance : FlowEntity
    {
        public FlowInstance()
        {
            Steps = new HashSet<FlowStep>();
        }

        public string Title { get; set; }
        public FlowType Type { get; set; }
        public Guid TypeId { get; set; }
        public string InitializerId { get; set; }
        public string AccessPhrase { get; set; }
        public string Payload { get; set; }
        public bool Active { get; set; }
        public ICollection<FlowStep> Steps { get; set; }
    }
}
