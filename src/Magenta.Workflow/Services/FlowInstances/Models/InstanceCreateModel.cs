using System;

namespace Magenta.Workflow.Services.FlowInstances.Models
{
    public class InstanceCreateModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string AccessPhrase { get; set; }
        public string Payload { get; set; }
        public string InitializerId { get; set; }
        public Guid TypeId { get; set; }
    }
}
