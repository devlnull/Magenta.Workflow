using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using System;
using System.Linq;
using System.Threading.Tasks;
using Magenta.Workflow.Managers.Reports;
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
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var targetType = MockData.GetFlowTypes()[0];
            //Act

            var act = await reportManager.GetTypeByIdAsync(targetType.Id);
            //Assert
            LogTestInfo(new { Request = targetType.Id }, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
        }

        [Fact]
        public async Task GetFlowTypeById_WithWrongId_MustNotReturnType()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var id = Guid.NewGuid();
            //Act
            var act = await reportManager.GetTypeByIdAsync(id);
            //Assert
            LogTestInfo(new { Request = id }, act);
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
        }

        [Fact]
        public async Task GetFlowType_WithCorrectExp_MustReturnType()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var targetType = MockData.GetFlowTypes()[0];
            //Act

            var act = await reportManager.GetTypeAsync(x => x.Id.Equals(targetType.Id));
            //Assert
            LogTestInfo(new { Request = targetType.Id }, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
        }

        [Fact]
        public async Task GetFlowType_WithWrongExp_MustNotReturnType()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var targetType = MockData.GetFlowTypes()[0];
            //Act
            var act = await reportManager
                .GetTypeAsync(x => x.Name == Guid.NewGuid().ToString());
            //Assert
            LogTestInfo(new { Request = targetType.Id }, act);
            Assert.False(act.Succeeded);
            Assert.Null(act.Result);
        }

        [Fact]
        public async Task GetFlowTypes_WithNoArgs_MustReturnAllTypes()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var targetTypes = MockData.GetFlowTypes();
            //Act

            var act = await reportManager.GetTypesAsync();
            //Assert
            LogTestInfo(result: act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.Contains(act.Result, x => x.States.Any());
            Assert.Equal(targetTypes.Length, act.Result.Count());
        }

        [Fact]
        public async Task GetPagedFlowTypes_WithNoArgs_MustReturnPagedAllTypes()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var targetTypes = MockData.GetFlowTypes();
            //Act

            var act = await reportManager.GetPagedTypesAsync(new PageOptions()
            {
                Limit = 10,
                Offset = 0
            });
            //Assert
            LogTestInfo(result: act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.Contains(act.Result.Items, x => x.States.Any());
            Assert.Equal(targetTypes.Length, act.Result.Count);
        }

        [Fact]
        public async Task GetFlowTypesByEntityType_WithMockEntityType_MustReturnBindType()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var targetTypes = MockData.GetFlowTypes()
                .Where(x => x.EntityType.Equals(typeof(MockState).FullName))
                .ToArray();
            //Act
            var act = await reportManager.GetTypesByEntityAsync(typeof(MockState));
            //Assert
            LogTestInfo(result: act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.Contains(act.Result, x => x.States.Any());
            Assert.Equal(targetTypes.Length, act.Result.Count());
        }

        [Fact]
        public async Task GetPagedFlowTypesByEntityType_WithMockEntityType_MustReturnBindType()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var reportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var targetTypes = MockData.GetFlowTypes()
                .Where(x=>x.EntityType.Equals(typeof(MockState).FullName))
                .ToArray();
            //Act

            var act = await reportManager.GetPagedTypesByEntityAsync(
                typeof(MockState),
                new PageOptions()
                {
                    Limit = 10,
                    Offset = 0
                });
            //Assert
            LogTestInfo(result: act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.Contains(act.Result.Items, x => x.States.Any());
            Assert.Equal(targetTypes.Length, act.Result.Count);
        }
    }
}
