namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowTypeRequest
    {
        public InitFlowTypeRequest() { }

        public InitFlowTypeRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
