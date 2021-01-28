using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Services.FlowInstances;
using Magenta.Workflow.Services.FlowTypes;
using Magenta.Workflow.UseCases;
using Magenta.Workflow.UseCases.InitFlow;
using Magenta.Workflow.UseCases.InitFlowType;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Managers.Flows
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(IStateManager stateManager)
        {
            StateManager = stateManager ?? throw new System.ArgumentNullException(nameof(stateManager));

            InstanceService = new FlowInstanceService(StateManager);
            TypeService = new FlowTypeService(StateManager);
        }

        public IStateManager StateManager { get; set; }
        public FlowInstanceService InstanceService { get; }
        public FlowTypeService TypeService { get; }

        #region Helpers

        private async Task<FlowResult<TResultModel>> HandleRequestAsync<TModel, TResultModel>(
            IFlowRequest<TModel, TResultModel> request, TModel model) 
            where TModel : class where TResultModel : class
        {
            if (request == null)
                throw new FlowException(FlowErrors.SERVICE_ISNULL, nameof(request));

            var validator = ObjectActivator.GetValidator<TModel>();

            //TODO: log the validate
            var validateResult = await validator.ValidateAsync(StateManager, model);
            if (!validateResult.Succeeded)
                return FlowResult<TResultModel>.Failed(validateResult.Errors.ToArray());

            //TODO: log the request 
            var requestResult = await request.DoAsync(model);

            //TODO: log the result
            if (validateResult.Warned)
                requestResult.Warns.AddRange(validateResult.Warns);

            return requestResult;
        }

        #endregion Helpers


        #region Init

        public async Task<FlowResult<FlowInstance>> InitFlowAsync(InitFlowModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowRequest(InstanceService), initModel);
            return result;
        }

        public async Task<FlowResult<FlowType>> InitFlowTypeAsync(InitFlowTypeModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowTypeRequest(TypeService), initModel);
            return result;
        }

        #endregion Init

    }
}