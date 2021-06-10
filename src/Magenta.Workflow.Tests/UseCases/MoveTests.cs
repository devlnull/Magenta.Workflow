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
        public async Task MoveStep_WithCorrectModel_MustMove()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var instance = MockData.GetFlowInstances().FirstOrDefault();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowReportManager = new ManagerFactory().GetFlowReportManager(stateManager);
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
            LogTestInfo(moveModel, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
        }

        [Fact]
        public async Task MoveStep_WithCorrectModel_MustHaveMoveAStep()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var instance = MockData.GetFlowInstances().FirstOrDefault();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowReportManager = new ManagerFactory().GetFlowReportManager(stateManager);
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
            var steps = await flowReportManager.GetInstanceStepsAsync(instance.Id);
            var currentStep = steps.Result.FirstOrDefault(x => x.IsCurrent);
            //Assert
            LogTestInfo(moveModel, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.Equal(moveModel.TransitionId, currentStep.TransitionId);
        }

        [Fact]
        public async Task MoveStep_WithIncorrectTarget_MustNotMove()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var instance = MockData.GetFlowInstances().FirstOrDefault();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowReportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var existTransitions = await flowReportManager.GetInstanceTransitionsAsync(instance.Id);
            var transitions = existTransitions.Result.Select(x => x.Id).ToList();
            var targetTransition = MockData.GetFlowTransitions()
                .FirstOrDefault(x => transitions.Contains(x.Id) == false);

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
            LogTestInfo(moveModel, act);
            Assert.False(act.Succeeded);
            Assert.NotEmpty(act.Errors);
        }

        [Fact]
        public async Task MoveStep_WithCorrectModel_MustChangeCurrentFlag()
        {
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var instance = MockData.GetFlowInstances().FirstOrDefault();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowReportManager = new ManagerFactory().GetFlowReportManager(stateManager);
            var existTransitions = await flowReportManager.GetInstanceTransitionsAsync(instance.Id);
            var targetTransition = existTransitions.Result.FirstOrDefault();
            var preSteps = await flowReportManager.GetInstanceStepsAsync(instance.Id);
            var preStep = preSteps.Result.FirstOrDefault(x => x.IsCurrent);

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
            var steps = await flowReportManager.GetInstanceStepsAsync(instance.Id);
            var currentStep = steps.Result.FirstOrDefault(x => x.IsCurrent);
            var updatedPreStep = steps.Result.FirstOrDefault(x => x.Id.Equals(preStep.Id));
            //Assert
            LogTestInfo(moveModel, act);
            Assert.True(act.Succeeded);
            Assert.NotNull(act.Result);
            Assert.True(currentStep.IsCurrent);
            Assert.False(updatedPreStep.IsCurrent);
            Assert.Equal(moveModel.TransitionId, currentStep.TransitionId);
        }
    }
}
