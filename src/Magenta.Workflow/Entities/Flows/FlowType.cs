using System;
using System.Collections.Generic;
using Magenta.Workflow.Entities.Base;

namespace Magenta.Workflow.Entities.Flows
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
        public ICollection<FlowInstance> Instances { get; set; }
    }
}