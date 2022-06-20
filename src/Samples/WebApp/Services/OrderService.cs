using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Services;

public class OrderService
{
    public Task<CreateOrderResult> CreateAsync(CreateOrderModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CreateOrderResult() {Id = Guid.NewGuid()});
    }

    public Task<ProviderRejectResult> ProviderRejectAsync(ProviderRejectModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ProviderRejectResult() {Id = Guid.NewGuid()});
    }

    public Task<ConfirmResult> ConfirmAsync(ConfirmModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ConfirmResult() {Id = Guid.NewGuid()});
    }

    public Task<FinalizeResult> FinalizeAsync(FinalizeModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new FinalizeResult() {Id = Guid.NewGuid()});
    }

    public Task<ExpireResult> ExpireAsync(ExpireModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ExpireResult() {Id = Guid.NewGuid()});
    }

    public Task<RefundByUserResult> RefundByUserAsync(RefundByUserModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new RefundByUserResult() {Id = Guid.NewGuid()});
    }

    public Task<ConfirmRejectResult> ConfirmRejectAsync(ConfirmRejectModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ConfirmRejectResult() {Id = Guid.NewGuid()});
    }
    
    public Task<CancelResult> CancelAsync(CancelModel model, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CancelResult() {Id = Guid.NewGuid()});
    }
}