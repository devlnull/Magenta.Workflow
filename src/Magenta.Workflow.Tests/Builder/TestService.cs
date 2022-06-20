using System;
using System.Threading;
using System.Threading.Tasks;

namespace Magenta.Workflow.Tests.Builder;

public class TestService
{
    public async Task<TestResult> TestAsync(TestModel model, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new TestResult() { Id = Guid.NewGuid()});
    }
}

public class TestModel
{
}

public class TestResult
{
    public Guid Id { get; set; }
}