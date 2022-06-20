using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.Builder
{
    public class FlowStateBuilder
    {
        internal FlowTransitionBuilder FlowTransitionBuilder { get; set; } = new();
        private FlowStateBuilderModel _lastStateModel;

        internal List<FlowStateBuilderModel> FlowStateBuilderModels { get; set; } = new();
        //TODO: Add Identity models to set identity of states

        internal FlowStateBuilder AddFlowState(FlowStateBuilderModel flowStateBuilderModel)
        {
            _lastStateModel = flowStateBuilderModel;
            FlowStateBuilderModels.Add(flowStateBuilderModel);
            return this;
        }

        public FlowStateBuilder AddFlowTransition(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Action> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            FlowTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, _lastStateModel);
            return this;
        }

        public FlowStateBuilder AddFlowTransition<TType, TArg1, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, Task<TResult>>> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            FlowTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, _lastStateModel);
            return this;
        }

        public FlowStateBuilder AddFlowTransition<TType, TArg1, TArg2, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, TArg2, Task<TResult>>> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            FlowTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, _lastStateModel);
            return this;
        }

        public FlowStateBuilder AddFlowTransition<TType, TArg1, TArg2, TArg3, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, TArg2, TArg3, Task<TResult>>> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            FlowTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, _lastStateModel);
            return this;
        }
    }
}