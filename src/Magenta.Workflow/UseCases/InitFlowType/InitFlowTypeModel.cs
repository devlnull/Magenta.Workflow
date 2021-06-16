namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowTypeModel
    {
        public InitFlowTypeModel() { }

        public InitFlowTypeModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
