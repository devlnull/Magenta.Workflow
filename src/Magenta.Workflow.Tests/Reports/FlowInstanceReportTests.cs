using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.Reports
{
    public class FlowInstanceReportTests : TestBase
    {
        public FlowInstanceReportTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        [Fact]
        public async Task GetFlowInstanceById_WithCorrectId_MustReturnInstance()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetInstance = MockData.GetFlowInstances()[0];
            //Act

            var act = await reportManager.GetInstanceByIdAsync(targetInstance.Id);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(new { Request = targetInstance.Id }, act);
        }

        [Fact]
        public async Task GetFlowInstanceById_WithWrongId_MustNotReturnInstance()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var id = Guid.NewGuid();
            //Act
            var act = await reportManager.GetInstanceByIdAsync(id);
            //Assert
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
            LogTestInfo(new { Request = id }, act);
        }

        [Fact]
        public async Task GetFlowInstance_WithCorrectExp_MustReturnInstance()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetInstance = MockData.GetFlowInstances()[0];
            //Act

            var act = await reportManager.GetInstanceAsync(x => x.Id.Equals(targetInstance.Id));
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(new { Request = targetInstance.Id }, act);
        }

        [Fact]
        public async Task GetFlowInstance_WithWrongExp_MustNotReturnInstance()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var id = Guid.NewGuid();
            //Act
            var act = await reportManager.GetInstanceAsync(x => x.Id == id);
            //Assert
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
            LogTestInfo(new { Request = id }, act);
        }
    }
}
