using System.Threading;
using System.Threading.Tasks;
using Magenta.Workflow.Builder;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.Tests.Infrastructures;
using Magenta.Workflow.Tests.Mock;
using Xunit;
using Xunit.Abstractions;

namespace Magenta.Workflow.Tests.Builder
{
    public class WorkflowBuilderTests : TestBase
    {
        public WorkflowBuilderTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        [Fact]
        public async Task WorkflowBuilder_WithCorrectModel_MustCreateWholeGraph()
        {
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);

            var workflowBuilder = new WorkflowBuilder("Order")
                .AddInitialTransition<TestService, TestModel, CancellationToken, TestResult>(
                    "Start", "Start", "Start", FlowTransitionTypes.Start,
                    (testService, model, cancellationToken) => testService.TestAsync(model, cancellationToken));
            workflowBuilder.AddFlowState("New", "New", FlowStateTypes.In)
                .AddFlowTransition<TestService, TestModel, CancellationToken, TestResult>(
                    "Confirmed", "Confirm", "Confirm", FlowTransitionTypes.Positive,
                    (testService, model, cancellationToken) => testService.TestAsync(model, cancellationToken))
                .AddFlowTransition<TestService, TestModel, CancellationToken, TestResult>(
                    "UnSuccessful", "Reject", "Reject", FlowTransitionTypes.Negative,
                    (testService, model, cancellationToken) => testService.TestAsync(model, cancellationToken));

            workflowBuilder.AddFlowState("Confirmed", "Confirmed", FlowStateTypes.InOut)
                .AddFlowTransition<TestService, TestModel, CancellationToken, TestResult>(
                    "Finalized", "Pay", "Pay", FlowTransitionTypes.Positive,
                    (testService, model, cancellationToken) => testService.TestAsync(model, cancellationToken))
                .AddFlowTransition<TestService, TestModel, CancellationToken, TestResult>(
                    "UnSuccessful", "Reject", "Reject", FlowTransitionTypes.Negative,
                    (testService, model, cancellationToken) => testService.TestAsync(model, cancellationToken));

            workflowBuilder.AddFlowState("UnSuccessful", "UnSuccessful", FlowStateTypes.Out);
            workflowBuilder.AddFlowState("Finalized", "Finalized", FlowStateTypes.Out);

            var result = await flowManager.BuildWorkflowAsync(workflowBuilder);

            Assert.True(result.Succeeded);
        }
    }
}