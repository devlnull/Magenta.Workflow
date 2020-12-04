namespace Sepid.Domain.Core.Tasks
{
    public class FlowTaskResult<TResult> : FlowTaskResult
    {
        public FlowTaskResult() { }

        public FlowTaskResult(TResult result)
        {
            Result = result;
        }

        public void SetResult(TResult result) => Result = result;

        public static new FlowTaskResult<TResult> Failed(params FlowTaskError[] errors)
        {
            var result = new FlowTaskResult<TResult>();
            if (errors != null)
            {
                result.Errors.AddRange(errors);
            }
            return result;
        }

        public static new FlowTaskResult<TResult> Warn(params FlowTaskWarn[] warns)
        {
            var result = new FlowTaskResult<TResult>();
            if (warns != null)
            {
                result.Warns.AddRange(warns);
            }
            return result;
        }

        public static FlowTaskResult<TResult> Successful(TResult result)
        {
            return new FlowTaskResult<TResult>()
            {
                Result = result,
                Succeeded = true,
            };
        }

        public FlowTaskResult<TResult> Merge(FlowTaskResult<TResult> secondResult)
        {
            var result = this;
            result.Errors.AddRange(secondResult.Errors);
            result.Warns.AddRange(secondResult.Warns);

            result.Succeeded = result.Succeeded && secondResult.Succeeded;
            result.Warned = result.Warned && secondResult.Warned;

            return result;
        }
        public static new FlowTaskResult<TResult> Success { get => new FlowTaskResult<TResult>(); }
    }
}
