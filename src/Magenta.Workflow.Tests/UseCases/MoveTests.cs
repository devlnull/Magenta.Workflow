using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Magenta.Workflow.UseCases.Move;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.UseCases
{
    public class MoveTests : TestBase
    {
        public MoveTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        [Fact]
        public async Task IntiFlowInstance_WithCorrectType_MustInitialize()
        {
            //Arrange
            var instance = MockData.GetFlowInstances().FirstOrDefault();
            var flowManager = ManagerFactory.GetFlowManager();
            var flowReportManager = ManagerFactory.GetFlowReportManager();
            var existTransitions = await flowReportManager.GetInstanceTransitionsAsync(instance.Id);
            var targetTransition = existTransitions.Result.FirstOrDefault();

            var moveModel = new MoveModel()
            {
                IdentityId = "1",
                InstanceId = instance.Id,
                Payload = string.Empty,
                TransitionId = targetTransition.Id,
                Comment = "Sure, It's ok.",
            };
            //Act
            var act = await flowManager.MoveAsync(moveModel);
            //Assert
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            LogTestInfo(moveModel, act);
        }
    }
}
