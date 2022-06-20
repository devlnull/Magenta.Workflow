using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Structures;

namespace Magenta.Workflow.Builder
{
    public class WorkflowBuilder
    {
        internal FlowStateBuilder FlowStateBuilder { get; set; } = new();
        internal WorkflowBuilderModel WorkflowBuilderModel { get; set; }
        private FlowTransitionBuilder InitialTransitionBuilder { get; set; } = new();

        public WorkflowBuilder(string flowTypeName)
        {
            WorkflowBuilderModel = new WorkflowBuilderModel(flowTypeName);
        }

        public WorkflowBuilder AddInitialTransition(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Action> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            InitialTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, null);
            return this;
        }

        public WorkflowBuilder AddInitialTransition<TType, TArg1, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, Task<TResult>>> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            InitialTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, null);
            return this;
        }

        public WorkflowBuilder AddInitialTransition<TType, TArg1, TArg2, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, TArg2, Task<TResult>>> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            InitialTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, null);
            return this;
        }

        public WorkflowBuilder AddInitialTransition<TType, TArg1, TArg2, TArg3, TResult>(
            string destination, string name, string title, FlowTransitionTypes transitionType,
            Expression<Func<TType, TArg1, TArg2, TArg3, Task<TResult>>> methodCall)
        {
            var flowTransitionBuilderModel = FlowTransitionBuilderModel.Init(
                destination, name, title, transitionType, methodCall);
            InitialTransitionBuilder.AddFlowTransition(flowTransitionBuilderModel, null);
            return this;
        }

        public FlowStateBuilder AddFlowState(string name, string title, FlowStateTypes stateType)
        {
            var stateBuilderModel = new FlowStateBuilderModel(name, title, stateType);
            FlowStateBuilder.AddFlowState(stateBuilderModel);
            return FlowStateBuilder;
        }
    }
}