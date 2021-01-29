using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Managers.States;

namespace Magenta.Workflow.Tests.Mock
{
    public class MockState
    {
        internal static IStateManager StateManager;
        public static IStateManager MockStateManager()
        {
            if (StateManager == null)
                StateManager = new InMemoryStateManager();
            StateManager = FillTypes(StateManager);


            return StateManager;
        }

        private static IStateManager FillTypes(IStateManager stateManager)
        {
            var set = stateManager.GetFlowSet<FlowType>();
            var items = MockData.GetFlowTypes();
            set.CreateListAsync(items).GetAwaiter().GetResult();
            return stateManager;
        }
    }
}
