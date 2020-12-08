using Magenta.Workflow.Services.FlowInstances;
using System;

namespace Magenta.Workflow.UseCases.Initializers
{
    public class InitFlow
    {
        private readonly FlowInstanceService _instanceService;

        public InitFlow(FlowInstanceService instanceService)
        {
            _instanceService = instanceService ?? throw new ArgumentNullException(nameof(instanceService));
        }

    }
}
