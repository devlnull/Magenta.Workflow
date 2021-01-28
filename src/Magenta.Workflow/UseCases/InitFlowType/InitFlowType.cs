using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowTypes;

namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowType
    {
        private readonly FlowTypeService _typeService;

        public InitFlowType(FlowTypeService typeService)
        {
            _typeService = typeService ?? throw new ArgumentNullException(nameof(typeService));
        }

        public Task<FlowResult<FlowType>> DoAsync<TEntity, TPayloadEntity>(string name)
        {
            return _typeService.CreateFlowTypeAsync<TEntity, TPayloadEntity>(name);
        }
    }
}
