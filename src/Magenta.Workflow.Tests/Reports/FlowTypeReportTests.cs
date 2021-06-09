using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.Reports
{
    public class FlowTypeReportTests : TestBase
    {
        public FlowTypeReportTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        [Fact]
        public async Task GetFlowTypeById_WithCorrectId_MustReturnType()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetType = MockData.GetFlowTypes()[0];
            //Act

            var act = await reportManager.GetTypeByIdAsync(targetType.Id);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(new { Request = targetType.Id }, act);
        }

        [Fact]
        public async Task GetFlowTypeById_WithWrongId_MustNotReturnType()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var id = Guid.NewGuid();
            //Act
            var act = await reportManager.GetTypeByIdAsync(id);
            //Assert
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
            LogTestInfo(new { Request = id }, act);
        }

        [Fact]
        public async Task GetFlowType_WithCorrectExp_MustReturnType()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetType = MockData.GetFlowTypes()[0];
            //Act

            var act = await reportManager.GetTypeAsync(x => x.Id.Equals(targetType.Id));
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(new { Request = targetType.Id }, act);
        }

        [Fact]
        public async Task GetFlowType_WithWrongExp_MustNotReturnType()
        {
            //Arrange
            var reportManager = ManagerFactory.GetFlowReportManager();
            var targetType = MockData.GetFlowTypes()[0];
            //Act
            var act = await reportManager.GetTypeAsync(x => x.Id != targetType.Id);
            //Assert
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
            LogTestInfo(new { Request = targetType.Id }, act);
        }
    }
}
