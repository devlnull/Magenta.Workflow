using System;
using System.Threading.Tasks;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.Reports
{
    public class FlowStateReportTests : TestBase
    {
        public FlowStateReportTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        [Fact]
        public async Task GetFlowStateById_WithCorrectId_MustReturnState()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetState = MockData.GetFlowStates()[0];
            //Act

            var act = await reportManager.GetStateByIdAsync(targetState.Id);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(new { Request = targetState.Id }, act);
        }

        [Fact]
        public async Task GetFlowStateById_WithWrongId_MustNotReturnState()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var id = Guid.NewGuid();
            //Act
            var act = await reportManager.GetStateByIdAsync(id);
            //Assert
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
            LogTestInfo(new { Request = id }, act);
        }

        [Fact]
        public async Task GetFlowState_WithCorrectExp_MustReturnState()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetState = MockData.GetFlowStates()[0];
            //Act

            var act = await reportManager.GetStateAsync(x => x.Id.Equals(targetState.Id));
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(new { Request = targetState.Id }, act);
        }

        [Fact]
        public async Task GetFlowState_WithWrongExp_MustNotReturnState()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetState = MockData.GetFlowStates()[0];
            //Act
            var act = await reportManager
                .GetStateAsync(x => x.Name == Guid.NewGuid().ToString());
            //Assert
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
            LogTestInfo(new { Request = targetState.Id }, act);
        }
    }
}
