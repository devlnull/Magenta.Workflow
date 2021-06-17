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
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowType = MockData.GetFlowTypes()[0];
            var approve1State = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Approve1");
            var activeState = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Active");

            var initModel = new InitFlowTransitionRequest()
            {
                Name = "FinalApprove",
                Title = "FinalApprove",
                SourceId = approve1State.Id,
                DestinationId = activeState.Id,
                TransitionType = FlowTransitionTypes.Reject,
                TypeId = flowType.Id
            };
            //Act
            var act = await flowManager.InitFlowTransitionAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
        }

        [Fact]
        public async Task IntiFlowTransition_WithExistingTransition_MustInitializeAndWarn()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowType = MockData.GetFlowTypes()[0];
            var reviewState = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Review");
            var closeState = MockData.GetFlowStates()
                .ToList().FirstOrDefault(x => x.Name == "Close");

            var initModel = new InitFlowTransitionRequest()
            {
                Name = "RejectTest",
                Title = "RejectTest",
                SourceId = reviewState.Id,
                DestinationId = closeState.Id,
                TransitionType = FlowTransitionTypes.Reject,
                TypeId = flowType.Id
            };
            //Act
            var act = await flowManager.InitFlowTransitionAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.True(act.Succeeded);
            Assert.NotEmpty(act.Warns);
        }
    }
}
