namespace Magenta.Workflow.Core.Tasks
{
    public class FlowResult<TResult> : FlowResult
    {
        public FlowResult() { }

        public FlowResult(TResult result)
        {
            Result = result;
        }

        public void SetResult(TResult result) => Result = result;

        public static new FlowResult<TResult> Failed(params FlowError[] errors)
        {
            var result = new FlowResult<TResult>();
            if (errors != null)
            {
                result.Errors.AddRange(errors);
            }
            return result;
        }

        public static new FlowResult<TResult> Warn(params FlowWarn[] warns)
        {
            var result = new FlowResult<TResult>();
            if (warns != null)
            {
                result.Warns.AddRange(warns);
            }
            return result;
        }

        public static FlowResult<TResult> Successful(TResult result)
        {
            return new FlowResult<TResult>()
            {
                Result = result,
                Succeeded = true,
            };
        }

        public FlowResult<TResult> Merge(FlowResult<TResult> secondResult)
        {
            var result = this;
            result.Errors.AddRange(secondResult.Errors);
            result.Warns.AddRange(secondResult.Warns);

            result.Succeeded = result.Succeeded && secondResult.Succeeded;
            result.Warned = result.Warned && secondResult.Warned;

            return result;
        }
        public static new FlowResult<TResult> Success { get => new FlowResult<TResult>(); }
    }
}
