using System;

namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowTypeModel
    {
        public string Name { get; set; }
        public Type EntityType { get; set; }
        public Type EntityPayloadType { get; set; }
    }
}
