using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Logger;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Magenta.Workflow.Managers.Flows
{
    public class FlowReportManager : IFlowReportManager
    {
        public FlowReportManager(IStateManager stateManager,
            IFlowLogger logger)
        {
            StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IStateManager StateManager { get; set; }
        public IFlowLogger Logger { get; set; }

        #region Transition Reports

        public async Task<FlowResult<IEnumerable<FlowTransition>>> GetInstanceTransitionsAsync(Guid instanceId)
        {
            //Get current instance
            if (instanceId.GuidIsEmpty())
                throw new ArgumentNullException(nameof(instanceId));

            var instanceSet = StateManager.GetFlowSet<FlowInstance>();

            var targetInstance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(instanceId));
            if (targetInstance == null)
                return FlowResult<IEnumerable<FlowTransition>>
                    .Failed(new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));

            //Get current instance state
            var stepSet = StateManager.GetFlowSet<FlowStep>();

            var currentStep = await stepSet
                .FirstOrDefaultAsync(x => x.InstanceId.Equals(targetInstance.Id) && x.IsCurrent);

            if (currentStep == null)
                return FlowResult<IEnumerable<FlowTransition>>
                    .Failed(new FlowError(FlowErrors.InstanceHasnostep));

            //Get current step transition
            var transitionSet = StateManager.GetFlowSet<FlowTransition>();

            var currentTransition = await transitionSet
                .FirstOrDefaultAsync(x => x.Id.Equals(currentStep.TransitionId));

            //Get current state
            var stateSet = StateManager.GetFlowSet<FlowState>();

            var currentState = await stateSet.FirstOrDefaultAsync(x => x.Id.Equals(currentTransition.DestinationId));

            //Get current state transitions
            var transitions = await transitionSet.GetAllAsync(x => x.SourceId.Equals(currentState.Id));

            return FlowResult<IEnumerable<FlowTransition>>.Successful(transitions);
        }

        #endregion Transition Reports

        #region Steps Reports

        public async Task<FlowResult<IEnumerable<FlowStep>>> GetInstanceStepsAsync(Guid instanceId)
        {
            //Get current instance
            if (instanceId.GuidIsEmpty())
                throw new ArgumentNullException(nameof(instanceId));

            var instanceSet = StateManager.GetFlowSet<FlowInstance>();

            var targetInstance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(instanceId));
            if (targetInstance == null)
                return FlowResult<IEnumerable<FlowStep>>
                    .Failed(new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));

            //Get all steps
            var stepSet = StateManager.GetFlowSet<FlowStep>();
            var steps = await stepSet.GetAllAsync(x => x.InstanceId.Equals(targetInstance.Id));

            return FlowResult<IEnumerable<FlowStep>>.Successful(steps);
        }

        #endregion Steps Reports

        #region Instance Reports

        public async Task<FlowResult<FlowInstance>> GetInstanceByIdAsync(Guid id)
        {
            try
            {
                Logger.LogInfo("try to get an instance of flow by id.");
                var instanceSet = StateManager.GetFlowSet<FlowInstance>();
                var instance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (instance == null)
                {
                    Logger.LogWarning("instance not exist.");
                    return FlowResult<FlowInstance>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));
                }
                var result = new FlowResult<FlowInstance>();
                if (instance.Active == false)
                {
                    Logger.LogWarning("instance is inactive");
                    result.Warns.Add(new FlowWarn(FlowWarns.InstanceInactive));
                }
                result.SetResult(instance);
                Logger.LogInfo($"instance with id '{instance.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowInstance>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<FlowInstance>> GetInstanceAsync(
            Expression<Func<FlowInstance,bool>> expression)
        {
            try
            {
                Logger.LogInfo("try to get an instance of flow by expression.");
                var instanceSet = StateManager.GetFlowSet<FlowInstance>();
                var instance = await instanceSet.FirstOrDefaultAsync(expression);
                if (instance == null)
                {
                    Logger.LogWarning("instance not exist.");
                    return FlowResult<FlowInstance>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));
                }
                var result = new FlowResult<FlowInstance>();
                if (instance.Active == false)
                {
                    Logger.LogWarning("instance is inactive");
                    result.Warns.Add(new FlowWarn(FlowWarns.InstanceInactive));
                }
                result.SetResult(instance);
                Logger.LogInfo($"instance with id '{instance.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowInstance>.Failed(new FlowError(ex.Message));
            }
        }

        #endregion Instance Reports

        #region State Reports

        public async Task<FlowResult<FlowState>> GetStateByIdAsync(Guid id)
        {
            try
            {
                Logger.LogInfo("try to get a state of flow by id.");
                var stateSet = StateManager.GetFlowSet<FlowState>();
                var state = await stateSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (state == null)
                {
                    Logger.LogWarning("state not exist.");
                    return FlowResult<FlowState>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowState)));
                }
                var result = new FlowResult<FlowState>();
                result.SetResult(state);
                Logger.LogInfo($"state with id '{state.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowState>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<FlowState>> GetStateAsync(
            Expression<Func<FlowState, bool>> expression)
        {
            try
            {
                Logger.LogInfo("try to get a state of flow by expression.");
                var stateSet = StateManager.GetFlowSet<FlowState>();
                var state = await stateSet.FirstOrDefaultAsync(expression);
                if (state == null)
                {
                    Logger.LogWarning("state not exist.");
                    return FlowResult<FlowState>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowState)));
                }
                var result = new FlowResult<FlowState>();
                result.SetResult(state);
                Logger.LogInfo($"state with id '{state.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowState>.Failed(new FlowError(ex.Message));
            }
        }

        #endregion State Reports

        #region Identity Reports

        #endregion Identity Reports

        #region Type Reports

        public async Task<FlowResult<FlowType>> GetTypeByIdAsync(Guid id)
        {
            try
            {
                Logger.LogInfo("try to get a type of flow by id.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var type = await typeSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (type == null)
                {
                    Logger.LogWarning("type not exist.");
                    return FlowResult<FlowType>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowType)));
                }
                var result = new FlowResult<FlowType>();
                result.SetResult(type);
                Logger.LogInfo($"type with id '{type.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowType>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<FlowType>> GetTypeAsync(
            Expression<Func<FlowType, bool>> expression)
        {
            try
            {
                Logger.LogInfo("try to get a type of flow by expression.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var type = await typeSet.FirstOrDefaultAsync(expression);
                if (type == null)
                {
                    Logger.LogWarning("type not exist.");
                    return FlowResult<FlowType>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowType)));
                }
                var result = new FlowResult<FlowType>();
                result.SetResult(type);
                Logger.LogInfo($"type with id '{type.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowType>.Failed(new FlowError(ex.Message));
            }
        }

        #endregion Type Reports

        #region Overview Reports

        // Number of instances, types, users, roles(groups)
        //Current status of flow

        #endregion Overview Reports

        #region Complex Reports

        // Flow history

        #endregion Complex Reports
    }
}
