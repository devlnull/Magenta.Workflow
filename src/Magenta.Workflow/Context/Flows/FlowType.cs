using System.Collections.Generic;
using System.Text.Json.Serialization;
using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Context.Flows
{
    public class FlowType : FlowEntity
    {
        public FlowType()
        {
            States = new HashSet<FlowState>();
            Instances = new HashSet<FlowInstance>();
            Transitions = new HashSet<FlowTransition>();
        }

        public string Name { get; set; }
        public ICollection<FlowState> States { get; set; }
        [JsonIgnore]
        public ICollection<FlowInstance> Instances { get; set; }
        [JsonIgnore]
        public ICollection<FlowTransition> Transitions { get; set; }
    }
}