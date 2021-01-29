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
            var flowManager = ManagerFactory.GetFlowManager();
            var flowType = MockData.GetFlowTypes()[0];
            var initModel = new InitFlowStateModel()
            {
                Name = "Test",
                Title = "Test Title",
                StateType = FlowStateTypes.Purposed,
                TypeGuidRow = flowType.GuidRow,
            };
            //Act

            var act = await flowManager.InitFlowStateAsync(initModel);
            //Assert
            Assert.True(act.Succeeded);
            LogTestInfo(initModel, act);
        }

        [Fact]
        public async Task IntiFlowState_WithWrongType_MustError()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var initModel = new InitFlowStateModel()
            {
                Name = "Test",
                Title = "Test Title",
                StateType = FlowStateTypes.Purposed,
                TypeGuidRow = Guid.NewGuid(),
            };
            //Act

            var act = await flowManager.InitFlowStateAsync(initModel);
            //Assert
            Assert.False(act.Succeeded);
            Assert.NotEmpty(act.Errors);
            LogTestInfo(initModel, act);
        }

        [Fact]
        public async Task IntiFlowState_WithEmptyName_MustError()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var initModel = new InitFlowStateModel()
            {
                Name = string.Empty,
                Title = "Test Title",
                StateType = FlowStateTypes.Purposed,
                TypeGuidRow = Guid.NewGuid(),
            };
            //Act

            var act = await flowManager.InitFlowStateAsync(initModel);
            //Assert
            Assert.False(act.Succeeded);
            Assert.NotEmpty(act.Errors);
            LogTestInfo(initModel, act);
        }
    }
}
