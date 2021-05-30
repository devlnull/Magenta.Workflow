using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Managers.States;

namespace Magenta.Workflow.Tests.Mock
{
    public class MockState
    {
        internal static IStateManager StateManager;
        private static readonly object locker = new object();
        public static IStateManager MockStateManager()
        {
            lock (locker)
            {
                if (StateManager == null)
                    StateManager = new InMemoryStateManager();
                if (StateManager.GetFlowSet<FlowType>().AnyAsync().GetAwaiter().GetResult() == false)
                    StateManager = FillTypes(StateManager);
                if (StateManager.GetFlowSet<FlowState>().AnyAsync().GetAwaiter().GetResult() == false)
                    StateManager = FillStates(StateManager);
                if (StateManager.GetFlowSet<FlowTransition>().AnyAsync().GetAwaiter().GetResult() == false)
                    StateManager = FillTransitions(StateManager);
                if (StateManager.GetFlowSet<FlowInstance>().AnyAsync().GetAwaiter().GetResult() == false)
                    StateManager = FillInstances(StateManager);
                if (StateManager.GetFlowSet<FlowStep>().AnyAsync().GetAwaiter().GetResult() == false)
                    StateManager = FillSteps(StateManager);

                return StateManager;
            }
        }

        private static IStateManager FillTypes(IStateManager stateManager)
        {
            var set = stateManager.GetFlowSet<FlowType>();
            var items = MockData.GetFlowTypes();
            set.CreateListAsync(items).GetAwaiter().GetResult();
            return stateManager;
        }

        private static IStateManager FillStates(IStateManager stateManager)
        {
            var set = stateManager.GetFlowSet<FlowState>();
            var items = MockData.GetFlowStates();
            set.CreateListAsync(items).GetAwaiter().GetResult();
            return stateManager;
        }

        private static IStateManager FillTransitions(IStateManager stateManager)
        {
            var set = stateManager.GetFlowSet<FlowTransition>();
            var items = MockData.GetFlowTransitions();
            set.CreateListAsync(items).GetAwaiter().GetResult();
            return stateManager;
        }

        private static IStateManager FillInstances(IStateManager stateManager)
        {
            var set = stateManager.GetFlowSet<FlowInstance>();
            var items = MockData.GetFlowInstances();
            set.CreateListAsync(items).GetAwaiter().GetResult();
            return stateManager;
        }

        private static IStateManager FillSteps(IStateManager stateManager)
        {
            var set = stateManager.GetFlowSet<FlowStep>();
            var items = MockData.GetFlowSteps();
            set.CreateListAsync(items).GetAwaiter().GetResult();
            return stateManager;
        }
    }
}
