using System;

namespace Magenta.Workflow.UseCases.Move
{
    public class MoveRequest
    {
        public Guid InstanceId { get; set; }
        public string Comment { get; set; }
        public string Payload { get; set; }
        public Guid TransitionId { get; set; }
        public string IdentityId { get; set; }
    }
}
