using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Magenta.Workflow.UseCases.InitFlowState;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.UseCases
{
    public class FlowStateInitializersTests : TestBase
    {
        public FlowStateInitializersTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }


        [Fact]
        public async Task IntiFlowState_WithCorrectModel_InitializeNewState()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowType = MockData.GetFlowTypes()[0];
            var initModel = new InitFlowStateModel()
            {
                Name = "Test",
                Title = "Test Title",
                StateType = FlowStateTypes.Purposed,
                TypeId = flowType.Id,
            };
            //Act

            var act = await flowManager.InitFlowStateAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.True(act.Succeeded);
        }

        [Fact]
        public async Task IntiFlowState_WithWrongType_MustError()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var initModel = new InitFlowStateModel()
            {
                Name = "Test",
                Title = "Test Title",
                StateType = FlowStateTypes.Purposed,
                TypeId = Guid.NewGuid(),
            };
            //Act

            var act = await flowManager.InitFlowStateAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.False(act.Succeeded);
            Assert.NotEmpty(act.Errors);
        }

        [Fact]
        public async Task IntiFlowState_WithEmptyName_MustError()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var initModel = new InitFlowStateModel()
            {
                Name = string.Empty,
                Title = "Test Title",
                StateType = FlowStateTypes.Purposed,
                TypeId = Guid.NewGuid(),
            };
            //Act

            var act = await flowManager.InitFlowStateAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.False(act.Succeeded);
            Assert.NotEmpty(act.Errors);
        }
    }
}
