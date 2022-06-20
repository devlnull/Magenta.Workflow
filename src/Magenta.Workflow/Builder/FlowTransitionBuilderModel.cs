using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Context.Structures;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.UseCases.InitFlowTransition;

namespace Magenta.Workflow.Builder
{
    public class FlowTransitionBuilderModel
    {
        public static FlowTransitionBuilderModel Init(string destination,
            string name, string title, FlowTransitionTypes transitionType,
            Expression<Action> methodCall)
        {
            var model = FlowTransitionBuilderModelHelper.FromExpression(methodCall, null);
            model.DestinationName = destination;
            model.Name = name;
            model.Title = title;
            model.TransitionType = transitionType;

            return model;
        }

        public static FlowTransitionBuilderModel Init<TType, TArg1, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, Task<TResult>>> methodCall)
        {
            var model = FlowTransitionBuilderModelHelper.FromExpression(methodCall, typeof(TType));
            model.DestinationName = destination;
            model.Name = name;
            model.Title = title;
            model.TransitionType = transitionType;

            return model;
        }

        public static FlowTransitionBuilderModel Init<TType, TArg1, TArg2, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, TArg2, Task<TResult>>> methodCall)
        {
            var model = FlowTransitionBuilderModelHelper.FromExpression(methodCall, typeof(TType));
            model.DestinationName = destination;
            model.Name = name;
            model.Title = title;
            model.TransitionType = transitionType;

            return model;
        }

        public static FlowTransitionBuilderModel Init<TType, TArg1, TArg2, TArg3, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, TArg2, TArg3, Task<TResult>>> methodCall)
        {
            var model = FlowTransitionBuilderModelHelper.FromExpression(methodCall, typeof(TType));
            model.DestinationName = destination;
            model.Name = name;
            model.Title = title;
            model.TransitionType = transitionType;

            return model;
        }

        internal string DestinationName { get; set; }
        internal string SourceName { get; set; }
        internal string Title { get; set; }
        internal string Name { get; set; }
        internal FlowTransitionTypes TransitionType { get; set; }
        internal Type Type { get; set; }
        internal MethodInfo Method { get; set; }
        internal IReadOnlyList<Type> Parameters { get; set; }

        public async Task<InitFlowTransitionRequest> MapToInitAsync(
            IFlowSet<FlowState> flowStateSet, Guid typeId)
        {
            var source = await flowStateSet
                .FirstOrDefaultAsync(x => x.Name.Equals(SourceName));
            var destination = await flowStateSet
                .FirstOrDefaultAsync(x => x.Name.Equals(DestinationName));

            //we don't care of existing source, destination, validator will handle it.

            var sourceId = source?.Id ?? Guid.NewGuid();
            var destinationId = destination?.Id ?? Guid.NewGuid();

            return new InitFlowTransitionRequest()
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