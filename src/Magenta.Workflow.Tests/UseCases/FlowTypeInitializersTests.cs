using System.Threading.Tasks;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Magenta.Workflow.UseCases.InitFlowType;
using Xunit;

namespace Magenta.Workflow.Tests.UseCases
{
    public class FlowTypeInitializersTests
    {
        [Fact]
        public async Task IntiFlowType_WithFillName_InitializeNewType()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var initModel = new InitFlowTypeModel()
            {
                Name = "Test",
                EntityPayloadType = typeof(FlowTypeInitializersTests),
                EntityType = typeof(FlowTypeInitializersTests),
            };
            //Act

            var result = await flowManager.InitFlowTypeAsync(initModel);
            //Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task IntiFlowType_DuplicateName_MustError()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var existType = MockData.GetFlowTypes()[0];
            var initModel = new InitFlowTypeModel()
            {
                Name = existType.Name,
                EntityPayloadType = typeof(FlowTypeInitializersTests),
                EntityType = typeof(FlowTypeInitializersTests),
            };
            //Act

            var result = await flowManager.InitFlowTypeAsync(initModel);
            //Assert
            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task IntiFlowType_EmptyModel_MustError()
        {
            //Arrange
            var flowManager = ManagerFactory.GetFlowManager();
            var initModel = new InitFlowTypeModel()
            {
                Name = null,
                EntityPayloadType = null,
                EntityType = null,
            };
            //Act

            var result = await flowManager.InitFlowTypeAsync(initModel);
            //Assert
            Assert.False(result.Succeeded);
        }
    }
}
