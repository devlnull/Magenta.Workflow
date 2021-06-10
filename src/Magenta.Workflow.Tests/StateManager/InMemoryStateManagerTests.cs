using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.StateManager
{
    public class InMemoryStateManagerTests : TestBase
    {
        public InMemoryStateManagerTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }
        
        [Fact]
        public async Task InMemoryStateManager_Initializer_CanGetFlowSet()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            //Act
            var flowSet = stateManager.GetFlowSet<FlowInstance>();
            //Assert
            var result = await flowSet.GetAllAsync();
            Assert.NotNull(result);
        }
    }
}
