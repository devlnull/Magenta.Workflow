﻿using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Services.FlowInstances;
using Magenta.Workflow.Services.FlowSteps;
using Magenta.Workflow.UseCases.Move;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlow
{
    public class InitFlowRequestHandler : IFlowRequestHandler<InitFlowRequest, FlowInstance>
    {
        private readonly IStateManager _stateManager;

        private FlowInstanceService InstanceService { get; }
        private FlowStepService StepService { get; }

        public InitFlowRequestHandler(
            IStateManager stateManager,
            FlowInstanceService instanceService,
            FlowStepService stepService)
        {
            _stateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
            InstanceService = instanceService ?? throw new ArgumentNullException(nameof(instanceService));
            StepService = stepService ?? throw new ArgumentNullException(nameof(stepService));
        }

        public async Task<FlowResult<FlowInstance>> DoAsync(InitFlowRequest request)
        {
            var taskResult = await InstanceService.CreateFlowInstanceAsync(request);

            var transitionSet = _stateManager.GetFlowSet<FlowTransition>();
            var startTransition = await transitionSet
                .FirstOrDefaultAsync(x => x.TypeId.Equals(taskResult.Result.TypeId)
                                        && x.TransitionType == FlowTransitionTypes.Start);

            if (startTransition == null)
                return FlowResult<FlowInstance>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, "Start transition"));

            var stepResult = await StepService.CreateFlowStepAsync(new MoveRequest()
            {
                Comment = null,
                IdentityId = request.InitializerId,
                InstanceId = taskResult.Result.Id,
                Payload = null,
                TransitionId = startTransition.Id
            });

            return taskResult.Merge(stepResult);
        }
    }
}
