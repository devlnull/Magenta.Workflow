using System.Collections.Generic;
using System.Linq;

namespace Magenta.Workflow.Core.Tasks
{
    public class FlowResult
    {
        private static readonly FlowResult _success = new FlowResult
        {
            _errors = new List<FlowError>(),
            _warns = new List<FlowWarn>()
        };
        private List<FlowError> _errors = new List<FlowError>();
        private List<FlowWarn> _warns = new List<FlowWarn>();

        private bool _succeeded;
        public bool Succeeded
        {
            get
            {
                _succeeded = !Errors.Any();
                return _succeeded;
            }
            set => _succeeded = value;
        }

        private bool _warned;
        public bool Warned
        {
            get
            {
                _warned = Warns.Any();
                return _warned;
            }
            set => _warned = value;
        }
        public virtual object Result { get; protected set; }
        private FlowResultTypes _type = FlowResultTypes.None;
        public FlowResultTypes Type
        {
            get
            {
                if (Errors.Any())
                    _type = FlowResultTypes.Error;
                else if (Warns.Any())
                    _type = FlowResultTypes.Warn;
                return _type;
            }
        }
        public List<FlowError> Errors => _errors;
        public List<FlowWarn> Warns => _warns;

        public static FlowResult Failed(params FlowError[] errors)
        {
            var result = new FlowResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        public static FlowResult Warn(params FlowWarn[] warns)
        {
            var result = new FlowResult { Succeeded = false, Warned = true };
            if (warns != null)
            {
                result._warns.AddRange(warns);
            }
            return result;
        }

        public static FlowResult Successful(object result)
        {
            return new FlowResult()
            {
                Result = result,
                Succeeded = true,
                _errors = new List<FlowError>() { },
            };
        }

        public static FlowResult Success => _success;

        public override string ToString()
        {
            switch (this.Type)
            {
                case FlowResultTypes.None:
                    return "Succeeded";
                case FlowResultTypes.Warn:
                    return $"{"Warned"} : {string.Join(",", Warns.Select(x => x.Message).ToList())}";
                case FlowResultTypes.Error:
                    return $"{"Failed"} : {string.Join(",", Errors.Select(x => x.Code).ToList())}";
                default:
                    return string.Empty;
            }
        }

        public FlowResult Merge(FlowResult secondResult)
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
