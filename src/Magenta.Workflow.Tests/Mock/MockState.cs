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
                StateManager = FillTypes(StateManager);
                StateManager = FillStates(StateManager);
                StateManager = FillTransitions(StateManager);


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
    }
}
