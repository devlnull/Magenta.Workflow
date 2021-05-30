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
            var flowManager = ManagerFactory.GetFlowManager();
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
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(initModel, act);
        }

        [Fact]
        public async Task IntiFlowInstance_EmptyPayload_MustWarn()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
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
            Assert.True(act.Warned);
            Assert.NotEmpty(act.Warns);
            LogTestInfo(initModel, act);
        }

        [Fact]
        public async Task FlowInstance_CorrectCreation_MustHaveInitializingStep()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var flowReportManager = ManagerFactory.GetFlowReportManager();
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
            var steps = await flowReportManager.GetInstanceStepsAsync(act.Result.Id);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.NotEmpty(steps.Result);
            LogTestInfo(initModel, act);
        }
    }
}
