using Magenta.Workflow.Tests.Mock;
using System.Threading.Tasks;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.UseCases.InitFlow;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.UseCases
{
    public class FlowInstanceInitializersTests : TestBase
    {
        public FlowInstanceInitializersTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        [Fact]
        public async Task IntiFlowInstance_WithCorrectType_MustInitialize()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var initModel = new InitFlowModel()
            {
                TypeId = MockData.GetFlowTypes()[0].Id,
                AccessPhrase = "secure",
                InitializerId = "1",
                Payload = "null",
                Title = "Hire devlnull"
            };
            //Act
            var act = await flowManager.InitFlowAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
        }

        [Fact]
        public async Task IntiFlowInstance_EmptyPayload_MustWarn()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var initModel = new InitFlowModel()
            {
                TypeId = MockData.GetFlowTypes()[0].Id,
                AccessPhrase = "secure",
                InitializerId = "1",
                Payload = null,
                Title = "Hire devlnull"
            };
            //Act
            var act = await flowManager.InitFlowAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.True(act.Warned);
            Assert.NotEmpty(act.Warns);
        }

        [Fact]
        public async Task FlowInstance_CorrectCreation_MustHaveInitializingStep()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var initModel = new InitFlowModel()
            {
                TypeId = MockData.GetFlowTypes()[0].Id,
                AccessPhrase = "secure",
                InitializerId = "1",
                Payload = "null",
                Title = "Hire devlnull"
            };
            //Act
            var act = await flowManager.InitFlowAsync(initModel);
            var flowReportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var steps = await flowReportManager.GetInstanceStepsAsync(act.Result.Id);
            //Assert
            LogTestInfo(initModel, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.NotEmpty(steps.Result);
        }
    }
}
