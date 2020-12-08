using Magenta.Workflow.Services.FlowTypes;
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
            var useCase = new Workflow.UseCases.Initializers.InitFlowType(new FlowTypeService());
            //Act
            var result = await useCase.DoAsync<FlowTypeInitializersTests, FlowTypeInitializersTests>("test");
            //Assert
            Assert.True(result.Succeeded);
        }
    }
}
