using System;
using System.Linq;
using System.Reflection;
using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Core.Validators;

namespace Magenta.Workflow.Utilities
{
    internal class ObjectActivator
    {
        internal static IFlowValidator<TModel> GetValidator<TModel>() where TModel : class
        {
            var modelType = typeof(TModel);
            var validatorModelType = typeof(IFlowValidator<TModel>);
            var assemblyTypes = modelType.Assembly.GetTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface);

            var validatorType = assemblyTypes
                .FirstOrDefault(x => x.GetTypeInfo().ImplementedInterfaces.Contains((validatorModelType)));

            if (validatorType == null)
                throw new FlowException(FlowErrors.ServiceIsnull, nameof(validatorType));

            var obj = Activator.CreateInstance(validatorType) as IFlowValidator<TModel>;
            if (obj == null)
                throw new FlowException(FlowErrors.ServiceIsnull, validatorType.Name);

            return obj;
        }
    }
}
