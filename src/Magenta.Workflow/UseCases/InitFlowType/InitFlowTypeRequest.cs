using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowTypes;

namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowTypeRequest : IFlowRequest<InitFlowTypeModel, FlowType>
    {
        private FlowTypeService TypeService { get; }

        public InitFlowTypeRequest(FlowTypeService typeService)
        {
            TypeService = typeService ?? throw new ArgumentNullException(nameof(typeService));
        }

        public Task<FlowResult<FlowType>> DoAsync(InitFlowTypeModel model)
        {
            return TypeService.CreateFlowTypeAsync(model);
        }
    }
}
