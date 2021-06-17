namespace Magenta.Workflow.Builder
{
    public class WorkflowBuilder
    {
        internal FlowStateBuilder FlowStateBuilder { get; set; } = new FlowStateBuilder();
        internal WorkflowBuilderModel WorkflowBuilderModel { get; set; }

        public WorkflowBuilder(string flowTypeName)
        {
            WorkflowBuilderModel = new WorkflowBuilderModel(flowTypeName);
        }

        public FlowStateBuilder AddFlowState(FlowStateBuilderModel flowStateBuilderModel)
        {
            FlowStateBuilder.AddFlowState(flowStateBuilderModel);
            return FlowStateBuilder;
        }
    }
}
