using System;
using System.Threading;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.UseCases.InitFlowState
{
    public class InitFlowStateRequest
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public FlowStateTypes StateType { get; set; }
        public Func<object,CancellationToken,Task<object>> Function { get; set; }
    }
}
