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
            //Arrange
            var stateManager = new MockState().MockStateManager();
            var flowManager = new ManagerFactory().GetFlowManager(stateManager);
            var flowReportManager = new ManagerFactory().GetFlowReportManager(stateManager);

            var workflowBuilder = new WorkflowBuilder("Order");
            workflowBuilder.AddFlowState(new FlowStateBuilderModel("New", "New", "New", FlowStateTypes.Purposed))
                .AddFlowTransition(new FlowTransitionBuilderModel("Confirmed", "Confirm", "Confirm", FlowTransitionTypes.Accept))
                .AddFlowTransition(new FlowTransitionBuilderModel("UnSuccessful", "Reject", "Reject", FlowTransitionTypes.Reject));
            
            workflowBuilder.AddFlowState(new FlowStateBuilderModel("Confirmed", "Confirmed", "Confirmed", FlowStateTypes.Active))
                .AddFlowTransition(new FlowTransitionBuilderModel("Finalized", "Pay", "Pay", FlowTransitionTypes.Accept))
                .AddFlowTransition(new FlowTransitionBuilderModel("UnSuccessful", "Reject", "Reject", FlowTransitionTypes.Reject));
            
            workflowBuilder.AddFlowState(new FlowStateBuilderModel("UnSuccessful", "UnSuccessful", "UnSuccessful", FlowStateTypes.Close));
            workflowBuilder.AddFlowState(new FlowStateBuilderModel("Finalized", "Finalized", "Finalized", FlowStateTypes.Close));
            //Act
            var result = await flowManager.BuildWorkflowAsync(workflowBuilder);
            //Assert
            Assert.True(result.Succeeded);
        }
    }
}
