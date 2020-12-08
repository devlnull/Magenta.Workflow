using System;

namespace Magenta.Workflow.Entities.Base
{
    public class FlowEntity
    {
        public long Id { get; set; }
        public Guid GuidRow { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
