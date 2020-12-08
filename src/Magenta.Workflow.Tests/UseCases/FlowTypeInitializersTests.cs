using Magenta.Workflow.Services.FlowTypes;
using Magenta.Workflow.Tests.Mock;
using System.Threading.Tasks;
using Xunit;

namespace Magenta.Workflow.Tests.UseCases
{
    public class FlowTypeInitializersTests
    {
        [Fact]
        public async Task IntiFlowType_WithFillName_InitializeNewType()
        {
            //Arrange
            var useCase = new Workflow.UseCases.Initializers.InitFlowType(new FlowTypeService(MockState.MockStateManager()));
            //Act
            var result = await useCase.DoAsync<FlowTypeInitializersTests, FlowTypeInitializersTests>("test");
            //Assert
            Assert.True(result.Succeeded);
        }
    }
}
