using System.Collections.Generic;
using Magenta.Workflow.Entities.Base;

namespace Magenta.Workflow.Entities.Flows
{
    public class FlowInstance : FlowEntity
    {
        public FlowInstance()
        {
            Steps = new HashSet<FlowStep>();
        }

        public string Title { get; set; }
        public FlowType Type { get; set; }
        public long TypeId { get; set; }
        public string InitializerId { get; set; }
        public string AccessPhrase { get; set; }
        public string Payload { get; set; }
        public bool Active { get; set; }
        public ICollection<FlowStep> Steps { get; set; }
    }
}
