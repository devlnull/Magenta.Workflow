using System.Linq;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Magenta.Workflow.UseCases.InitFlowTransition;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.UseCases
{

    public class FlowTransitionInitializersTests : TestBase
    {
        public FlowTransitionInitializersTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        [Fact]
        public async Task IntiFlowTransition_WithCorrectModel_MustInitialize()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var approve1State = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Approve1");
            var activeState = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Active");

            var initModel = new InitFlowTransitionModel()
            {
                Name = "FinalApprove",
                Title = "FinalApprove",
                SourceGuidRow = approve1State.GuidRow,
                DestinationGuidRow = activeState.GuidRow,
                TransitionType = FlowTransitionTypes.Reject,
            };
            //Act
            var act = await flowManager.InitFlowTransitionAsync(initModel);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(initModel, act);
        }

        [Fact]
        public async Task IntiFlowTransition_WithExistingTransition_MustInitializeAndWarn()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var reviewState = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Review");
            var closeState = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Close");

            var initModel = new InitFlowTransitionModel()
            {
                Name = "RejectTest",
                Title = "RejectTest",
                SourceGuidRow = reviewState.GuidRow,
                DestinationGuidRow = closeState.GuidRow,
                TransitionType = FlowTransitionTypes.Reject,
            };
            //Act
            var act = await flowManager.InitFlowTransitionAsync(initModel);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotEmpty(act.Warns);
            LogTestInfo(initModel, act);
        }
    }
}
