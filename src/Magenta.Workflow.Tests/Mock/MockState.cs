using Magenta.Workflow.Entities.Flows;
using Magenta.Workflow.Managers.States.Abstracts;
using Magenta.Workflow.Managers.States.Concrete;

namespace Magenta.Workflow.Tests.Mock
{
    public class MockState
    {
        public static IStateManager MockStateManager()
        {
            var memoryStateManager = new InMemoryStateManager();
            memoryStateManager = FillTypes(memoryStateManager);


            return memoryStateManager;
        }

        private static InMemoryStateManager FillTypes(InMemoryStateManager memoryStateManager)
        {
            var set = memoryStateManager.GetFlowSet<FlowType>();
            var items = MockData.GetFlowTypes();
            set.CreateListAsync(items).GetAwaiter().GetResult();
            return memoryStateManager;
        }
    }
}
