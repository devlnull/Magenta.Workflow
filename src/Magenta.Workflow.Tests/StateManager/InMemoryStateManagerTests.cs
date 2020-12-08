using Magenta.Workflow.Entities.Flows;
using Magenta.Workflow.Managers.States.Abstracts;
using Magenta.Workflow.Managers.States.Concrete;
using System.Threading.Tasks;
using Xunit;

namespace Magenta.Workflow.Tests.StateManager
{
    public class InMemoryStateManagerTests
    {
        [Fact]
        public async Task InMemoryStateManager_Initializer_CanGetFlowSet()
        {
            //Arrange
            IStateManager stateManager = new InMemoryStateManager();
            //Act
            var flowSet = stateManager.GetFlowSet<FlowInstance>();
            //Assert
            var result = await flowSet.GetAllAsync();
            Assert.NotNull(result);
        }
    }
}
