using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Entities.Flows;
using Magenta.Workflow.Services.FlowInstances;
using Magenta.Workflow.Services.FlowInstances.Models;
using Magenta.Workflow.UseCases.Initializers.Models;
using System;
using System.Threading.Tasks;

namespace Magenta.Workflow.UseCases.Initializers
{
    public class InitFlow
    {
        private readonly FlowInstanceService _instanceService;

        public InitFlow(FlowInstanceService instanceService)
        {
            _instanceService = instanceService ?? throw new ArgumentNullException(nameof(instanceService));
        }

        public async Task<FlowResult<FlowInstance>> DoAsync(InitFlowModel model)
        {
            var instanceModel = new InstanceCreateModel()
            {
                AccessPhrase = model.AccessPhrase,
                InitializerId = model.InitializerId,
                Name = model.Name,
                Title = model.Title,
                Payload = model.Payload,
                TypeId = model.TypeId
            };
            
            //TODO: take first step of instance.

            var taskResult = await _instanceService.CreateFlowInstanceAsync(instanceModel);
            return taskResult;
        }
    }
}
