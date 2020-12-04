using System.Collections.Generic;
using Magenta.Workflow.DataTransferObjects.Base;

namespace Magenta.Workflow.DataTransferObjects.Flows
{
    public class FlowState : FlowEntity
    {
        public FlowState()
        {
            Sources = new HashSet<FlowTransition>();
            Destinations = new HashSet<FlowTransition>();
            Identities = new HashSet<FlowIdentity>();
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public byte StateType { get; set; }
        public FlowType Type { get; set; }
        public long TypeId { get; set; }
        public ICollection<FlowIdentity> Identities { get; set; }
        public ICollection<FlowTransition> Sources { get; set; }
        public ICollection<FlowTransition> Destinations { get; set; }
    }
}
