using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowTypes;

namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowTypeRequestHandler : IFlowRequestHandler<InitFlowTypeRequest, FlowType>
    {
        private FlowTypeService TypeService { get; }

        public InitFlowTypeRequestHandler(FlowTypeService typeService)
        {
            TypeService = typeService ?? throw new ArgumentNullException(nameof(typeService));
        }

        public Task<FlowResult<FlowType>> DoAsync(InitFlowTypeRequest request)
        {
            return TypeService.CreateFlowTypeAsync(request);
        }
    }
}
