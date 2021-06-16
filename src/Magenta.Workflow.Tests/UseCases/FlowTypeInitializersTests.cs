using System.Threading.Tasks;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Magenta.Workflow.UseCases.InitFlowType;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.UseCases
{
    public class FlowTypeInitializersTests : TestBase
    {
        public FlowTypeInitializersTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }


        [Fact]
        public async Task IntiFlowType_WithFillName_InitializeNewType()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var initModel = new InitFlowTypeModel()
            {
                Name = "Test",
            };
            //Act

            var act = await flowManager.InitFlowTypeAsync(initModel);
            //Assert
            LogTestInfo( initModel, act);
            Assert.True(act.Succeeded);
        }

        [Fact]
        public async Task IntiFlowType_DuplicateName_MustError()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var existType = MockData.GetFlowTypes()[0];
            var initModel = new InitFlowTypeModel()
            {
                Name = existType.Name,
            };
            //Act

            var act = await flowManager.InitFlowTypeAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.False(act.Succeeded);
        }

        [Fact]
        public async Task IntiFlowType_EmptyModel_MustError()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var initModel = new InitFlowTypeModel()
            {
                Name = null,
            };
            //Act

            var act = await flowManager.InitFlowTypeAsync(initModel);
            //Assert
            LogTestInfo(initModel, act);
            Assert.False(act.Succeeded);
        }
    }
}
