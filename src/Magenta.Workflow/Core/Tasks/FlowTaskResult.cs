using System.Collections.Generic;
using System.Linq;

namespace Sepid.Domain.Core.Tasks
{
    public class FlowTaskResult
    {
        private static readonly FlowTaskResult _success = new FlowTaskResult
        {
            _errors = new List<FlowTaskError>(),
            _warns = new List<FlowTaskWarn>()
        };
        private List<FlowTaskError> _errors = new List<FlowTaskError>();
        private List<FlowTaskWarn> _warns = new List<FlowTaskWarn>();

        private bool _succeeded;
        public bool Succeeded
        {
            get
            {
                _succeeded = !Errors.Any() && !Warns.Any();
                return _succeeded;
            }
            set
            {
                _succeeded = value;
            }
        }

        private bool _warned;
        public bool Warned
        {
            get
            {
                _warned = Warns.Any();
                return _warned;
            }
            set
            {
                _warned = value;
            }
        }
        public virtual object Result { get; protected set; }
        private FlowTaskResultTypes _type = FlowTaskResultTypes.None;
        public FlowTaskResultTypes Type
        {
            get
            {
                if (Errors.Any())
                    _type = FlowTaskResultTypes.Error;
                else if (Warns.Any())
                    _type = FlowTaskResultTypes.Warn;
                return _type;
            }
        }
        public List<FlowTaskError> Errors => _errors;
        public List<FlowTaskWarn> Warns => _warns;

        public static FlowTaskResult Failed(params FlowTaskError[] errors)
        {
            var result = new FlowTaskResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        public static FlowTaskResult Warn(params FlowTaskWarn[] warns)
        {
            var result = new FlowTaskResult { Succeeded = false, Warned = true };
            if (warns != null)
            {
                result._warns.AddRange(warns);
            }
            return result;
        }

        public static FlowTaskResult Successful(object result)
        {
            return new FlowTaskResult()
            {
                Result = result,
                Succeeded = true,
                _errors = new List<FlowTaskError>() { },
            };
        }

        public static FlowTaskResult Success => _success;

        public override string ToString()
        {
            switch (this.Type)
            {
                case FlowTaskResultTypes.None:
                    return "Succeeded";
                case FlowTaskResultTypes.Warn:
                    return string.Format("{0} : {1}", "Warned", string.Join(",", Warns.Select(x => x.Message).ToList()));
                case FlowTaskResultTypes.Error:
                    return string.Format("{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Code).ToList()));
                default:
                    return string.Empty;
            }
        }

        public FlowTaskResult Merge(FlowTaskResult secondResult)
        {
            var result = this;
            result.Errors.AddRange(secondResult.Errors);
            result.Warns.AddRange(secondResult.Warns);

            result.Succeeded = result.Succeeded && secondResult.Succeeded;
            result.Warned = result.Warned && secondResult.Warned;

            return result;
        }
    }
}
