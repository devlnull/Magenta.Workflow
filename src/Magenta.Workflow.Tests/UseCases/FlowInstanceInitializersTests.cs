using Magenta.Workflow.Tests.Mock;
using System.Threading.Tasks;
using Magenta.Workflow.Tests.Infrastructures;
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
            var flowManager = ManagerFactory.GetFlowManager();
            var initModel = new InitFlowModel()
            {
                TypeId = MockData.GetFlowTypes()[0].GuidRow,
                AccessPhrase = "secure",
                InitializerId = "1",
                Payload = "null",
                Title = "Hire devlnull"
            };
            //Act
            var act = await flowManager.InitFlowAsync(initModel);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            
        }

        [Fact]
        public async Task IntiFlowInstance_EmptyPayload_MustWarn()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var initModel = new InitFlowModel()
            {
                TypeId = MockData.GetFlowTypes()[0].GuidRow,
                AccessPhrase = "secure",
                InitializerId = "1",
                Payload = null,
                Title = "Hire devlnull"
            };
            //Act
            var act = await flowManager.InitFlowAsync(initModel);
            //Assert
            Assert.True(act.Warned);
            Assert.NotEmpty(act.Warns);
        }
    }
}
