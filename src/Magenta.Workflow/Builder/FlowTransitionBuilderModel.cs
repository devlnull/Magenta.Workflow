using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.UseCases.InitFlowTransition;

namespace Magenta.Workflow.Builder
{
    public class FlowTransitionBuilderModel
    {
        public FlowTransitionBuilderModel() { }

        public FlowTransitionBuilderModel(string destination,
            string name, string title, FlowTransitionTypes transitionType)
        {
            DestinationName = destination;
            Title = title;
            Name = name;
            TransitionType = transitionType;
        }

        public string DestinationName { get; set; }
        internal string SourceName { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public FlowTransitionTypes TransitionType { get; set; }

        public async Task<InitFlowTransitionModel> MapToInitAsync(
            IFlowSet<FlowState> flowStateSet, Guid typeId)
        {
            var source = await flowStateSet
                .FirstOrDefaultAsync(x => x.Name.Equals(SourceName));
            var destination = await flowStateSet
                .FirstOrDefaultAsync(x=>x.Name.Equals(DestinationName));

            //we don't care of existing source, destination, validator will handle it.

            Guid sourceId = source?.Id ?? Guid.NewGuid();
            Guid destinationId = destination?.Id ?? Guid.NewGuid();

            return new InitFlowTransitionModel()
            {
                SourceId = sourceId,
                DestinationId = destinationId,
                Name = Name,
                Title = Title,
                TransitionType = TransitionType,
                TypeId = typeId
            };
        }
    }
}
