using Magenta.Workflow.UseCases.InitFlowType;

namespace Magenta.Workflow.Builder
{
    public class WorkflowBuilderModel
    {
        public WorkflowBuilderModel() { }

        public WorkflowBuilderModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public InitFlowTypeRequest MapToInit()
        {
            return new InitFlowTypeRequest(Name);
        }
    }
}
