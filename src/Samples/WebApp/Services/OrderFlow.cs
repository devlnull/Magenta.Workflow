using System.Threading;
using System.Threading.Tasks;
using Magenta.Workflow;
using Magenta.Workflow.Builder;
using Magenta.Workflow.Context.Structures;

namespace WebApp.Services;

public class OrderFlow
{
    public async Task<bool> CreateFlowAsync(IFlowManager flowManager)
    {
        var orderFlow = new WorkflowBuilder("OrderFlow")
            .AddInitialTransition<OrderService, CreateOrderModel, CancellationToken, CreateOrderResult>(
                "Initialized", "Initializer", "Initializer", FlowTransitionTypes.Start,
                (orderService, model, cancellationToken) => orderService.CreateAsync(model, cancellationToken));

        orderFlow.AddFlowState("Initialized", "Initialized", FlowStateTypes.In)
            .AddFlowTransition<OrderService, ConfirmModel, CancellationToken, ConfirmResult>(
                "Confirmed", "Confirm", "Confirm", FlowTransitionTypes.Positive,
                (orderService, model, cancellationToken) => orderService.ConfirmAsync(model, cancellationToken));

        orderFlow.AddFlowState("Confirmed", "Confirmed", FlowStateTypes.InOut)
            .AddFlowTransition<OrderService, ProviderRejectModel, CancellationToken, ProviderRejectResult>(
                "Failed", "ProviderReject", "ProviderReject", FlowTransitionTypes.Negative,
                (orderService, model, cancellationToken) => orderService.ProviderRejectAsync(model, cancellationToken))
            .AddFlowTransition<OrderService, ExpireModel, CancellationToken, ExpireResult>(
                "Unknown", "Expire", "Expire", FlowTransitionTypes.Negative,
                (orderService, model, cancellationToken) => orderService.ExpireAsync(model, cancellationToken))
            .AddFlowTransition<OrderService, ConfirmRejectModel, CancellationToken, ConfirmRejectResult>(
                "Canceled", "ConfirmReject", "ConfirmReject", FlowTransitionTypes.Negative,
                (orderService, model, cancellationToken) => orderService.ConfirmRejectAsync(model, cancellationToken))
            .AddFlowTransition<OrderService, FinalizeModel, CancellationToken, FinalizeResult>(
                "Finalized", "Finalize", "Finalize", FlowTransitionTypes.Negative,
                (orderService, model, cancellationToken) => orderService.FinalizeAsync(model, cancellationToken));

        orderFlow.AddFlowState("Finalized", "Finalized", FlowStateTypes.InOut)
            .AddFlowTransition<OrderService, CancelModel, CancellationToken, CancelResult>(
                "Canceled", "CancelManually", "CancelManually", FlowTransitionTypes.Negative,
                (orderService, model, cancellationToken) => orderService.CancelAsync(model, cancellationToken))
            .AddFlowTransition<OrderService, RefundByUserModel, CancellationToken, RefundByUserResult>(
                "Refunded", "RefundByUser", "RefundByUser", FlowTransitionTypes.Negative,
                (orderService, model, cancellationToken) => orderService.RefundByUserAsync(model, cancellationToken));

        orderFlow.AddFlowState("Failed", "Failed", FlowStateTypes.Out);
        orderFlow.AddFlowState("Unknown", "Unknown", FlowStateTypes.Out);
        orderFlow.AddFlowState("Canceled", "Canceled", FlowStateTypes.Out);
        orderFlow.AddFlowState("Refunded", "Refunded", FlowStateTypes.Out);

        var orderFlowBuildResult = await flowManager.BuildWorkflowAsync(orderFlow);
        return orderFlowBuildResult.Succeeded;
    }
}