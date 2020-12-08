using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Entities.Flows;
using Magenta.Workflow.Services.FlowTypes;
using System;
using System.Threading.Tasks;

namespace Magenta.Workflow.UseCases.Initializers
{
    public class InitFlowType
    {
        private readonly FlowTypeService _typeService;

        public InitFlowType(FlowTypeService typeService)
        {
            _typeService = typeService ?? throw new ArgumentNullException(nameof(typeService));
        }

        public Task<FlowTaskResult<FlowType>> DoAsync<TEntity, TPayloadEntity>(string name)
        {
            return _typeService.CreateFlowTypeAsync<TEntity, TPayloadEntity>(name);
        }
    }
}
