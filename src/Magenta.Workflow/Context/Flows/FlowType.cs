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
        }

        public string Name { get; set; }
        public string EntityType { get; set; }
        public string EntityPayloadType { get; set; }
        public ICollection<FlowState> States { get; set; }
        [JsonIgnore]
        public ICollection<FlowInstance> Instances { get; set; }
    }
}