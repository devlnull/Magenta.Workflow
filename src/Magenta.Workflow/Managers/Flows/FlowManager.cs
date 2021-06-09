using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Core.Logger;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Services.FlowInstances;
using Magenta.Workflow.Services.FlowStates;
using Magenta.Workflow.Services.FlowSteps;
using Magenta.Workflow.Services.FlowTransitions;
using Magenta.Workflow.Services.FlowTypes;
using Magenta.Workflow.UseCases;
using Magenta.Workflow.UseCases.InitFlow;
using Magenta.Workflow.UseCases.InitFlowState;
using Magenta.Workflow.UseCases.InitFlowTransition;
using Magenta.Workflow.UseCases.InitFlowType;
using Magenta.Workflow.UseCases.Move;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Managers.Flows
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(IStateManager stateManager, IFlowLogger logger)
        {
            StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InstanceService = new FlowInstanceService(StateManager);
            TypeService = new FlowTypeService(StateManager);
            StateService = new FlowStateService(StateManager);
            TransitionService = new FlowTransitionService(StateManager);
            StepService = new FlowStepService(StateManager);
        }

        public IFlowLogger Logger { get; set; }
        public IStateManager StateManager { get; set; }
        public FlowInstanceService InstanceService { get; }
        public FlowTypeService TypeService { get; }
        public FlowStateService StateService { get; }
        public FlowTransitionService TransitionService { get; }
        public FlowStepService StepService { get; set; }

        #region Helpers

        private async Task<FlowResult<TResultModel>> HandleRequestAsync<TModel, TResultModel>(
            IFlowRequest<TModel, TResultModel> request, TModel model)
            where TModel : class where TResultModel : class
        {
            try
            {
                Logger.LogInfo(FlowLogs.RequestStarted, args: model.GetType().Name);
                if (request == null)
                    throw new FlowException(FlowErrors.ServiceIsnull, nameof(request));

                var validator = ObjectActivator.GetValidator<TModel>();

                var validateResult = await validator.ValidateAsync(StateManager, model);

                Logger.LogInfo(FlowLogs.RequestHasWarn, args: validateResult.Warns.Count.ToString());
                Logger.LogInfo(FlowLogs.RequestHasError, args: validateResult.Errors.Count.ToString());

                if (!validateResult.Succeeded)
                    return FlowResult<TResultModel>.Failed(validateResult.Errors.ToArray());


                Logger.LogInfo(FlowLogs.RequestOperationStarted, args: model.GetType().Name);
                var requestResult = await request.DoAsync(model);
                Logger.LogInfo(FlowLogs.RequestOperationFinished, args: model.GetType().Name);

                if (validateResult.Warned)
                    requestResult.Warns.AddRange(validateResult.Warns);

                Logger.LogInfo(FlowLogs.RequestFinished, args: model.GetType().Name);
                return requestResult;
            }
            catch (Exception ex)
            {
                Logger.LogError(FlowLogs.ExceptionOccured, ex.Message);
                return FlowResult<TResultModel>.Failed(new FlowError(FlowErrors.ErrorOccured));
            }
        }

        #endregion Helpers


        #region Init

        public async Task<FlowResult<FlowInstance>> InitFlowAsync(InitFlowModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowRequest(StateManager, InstanceService, StepService),
                initModel);
            return result;
        }

        public async Task<FlowResult<FlowType>> InitFlowTypeAsync(InitFlowTypeModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowTypeRequest(TypeService), initModel);
            return result;
        }

        public async Task<FlowResult<FlowState>> InitFlowStateAsync(InitFlowStateModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowStateRequest(StateService), initModel);
            return result;
        }

        public async Task<FlowResult<FlowTransition>> InitFlowTransitionAsync(InitFlowTransitionModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowTransitionRequest(TransitionService), initModel);
            return result;
        }

        #endregion Init

        #region Move

        public async Task<FlowResult<FlowStep>> MoveAsync(MoveModel moveModel)
        {
            var result = await HandleRequestAsync(new MoveRequest(StepService), moveModel);
            return result;
        }

        #endregion Move

    }
}