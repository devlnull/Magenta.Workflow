using Magenta.Workflow.Services.FlowInstances;
using Magenta.Workflow.Tests.Mock;
using System.Linq;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.UseCases.InitFlow;
using Xunit;

namespace Magenta.Workflow.Tests.UseCases
{
    public class FlowInstanceInitializersTests
    {
        [Fact]
        public async Task IntiFlowInstance_WithCorrectType_MustInitialize()
        {
            //Arrange
            var state = MockState.MockStateManager();
            var set = state.GetFlowSet<FlowInstance>();
            var useCase = new InitFlow(new FlowInstanceService(state));
            //Act
            var result = await useCase.DoAsync(new InitFlowModel()
            {
                TypeId = MockData.GetFlowTypes()[0].GuidRow,
                AccessPhrase = "secure",
                InitializerId = "1",
                Name = "Hire Me",
                Payload = "null",
                Title = "Hire devlnull"
            });
            bool hasInserted = set.GetAll().Any(x => x.GuidRow.Equals((result.Result as FlowInstance).GuidRow));
            //Assert
            Assert.True(result.Succeeded);
            Assert.True(hasInserted);
            
        }
    }
}
