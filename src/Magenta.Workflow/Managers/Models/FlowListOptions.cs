using System;

namespace Magenta.Workflow.Managers.Models
{
    public class FlowListOptions
    {
        public Guid? InstanceGuidRow { get; set; }
        public string Payload { get; set; }
        public string StateTag { get; set; }
        public string PreviousStateTag { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
