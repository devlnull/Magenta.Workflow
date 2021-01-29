using System;
using Magenta.Workflow.Context.Flows;

namespace Magenta.Workflow.Managers.Flows
{
    public class FlowCartableModel
    {
        public Guid GuidRow { get; set; }
        public string Title { get; set; }
        public string TypeName { get; set; }
        public Guid TypeGuidRow { get; set; }
        public string Initializer { get; set; }
        public bool Active { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public byte StateType { get; set; }
        public Guid? StateGuidRow { get; set; }
        public string PreviousState { get; set; }
        public string PreviousStateName { get; set; }
        public byte PreviousStateType { get; set; }
        public string TransitionTitle { get; set; }
        public string TransitionName { get; set; }
        public Guid? TransitionGuidRow { get; set; }
        public DateTime CreateDate { get; set; }
        public string Payload { get; set; }
        public FlowInstance Instance { get; set; }
        public long StateId { get; set; }
    }
}
