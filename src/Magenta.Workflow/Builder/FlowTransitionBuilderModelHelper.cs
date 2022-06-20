using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hangfire.Annotations;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Builder;

public static class FlowTransitionBuilderModelHelper
{
    public static FlowTransitionBuilderModel FromExpression([NotNull] LambdaExpression methodCall,
        [CanBeNull] Type explicitType)
    {
        if (methodCall == null) throw new ArgumentNullException(nameof(methodCall));

        var callExpression = methodCall.Body as MethodCallExpression;
        if (callExpression == null)
            throw new ArgumentException("Expression body should be of type `MethodCallExpression`", nameof(methodCall));

        var type = explicitType ?? callExpression.Method.DeclaringType;
        var method = callExpression.Method;

        if (explicitType == null && callExpression.Object != null)
        {
            var objectValue = GetExpressionValue(callExpression.Object);
            if (objectValue == null)
                throw new InvalidOperationException("Expression object should be not null.");
            type = objectValue.GetType();

            method = type.GetNonOpenMatchingMethod(
                callExpression.Method.Name,
                callExpression.Method.GetParameters().Select(x => x.ParameterType).ToArray());
        }

        return new FlowTransitionBuilderModel
        {
            Type = type,
            Method = method,
            Parameters = callExpression.Arguments.Select(x => x.Type).ToArray()
        };
    }

    public static void Validate(
        Type type,
        [InvokerParameterName] string typeParameterName,
        MethodInfo method,
        [InvokerParameterName] string methodParameterName,
        int argumentCount,
        [InvokerParameterName] string argumentParameterName)
    {
        if (!method.IsPublic)
        {
            throw new NotSupportedException(
                "Only public methods can be invoked in the background. Ensure your method has the `public` access modifier, and you aren't using explicit interface implementation.");
        }

        if (method.ContainsGenericParameters)
        {
            throw new NotSupportedException("Job method can not contain unassigned generic type parameters.");
        }

        if (method.DeclaringType == null)
        {
            throw new NotSupportedException("Global methods are not supported. Use class methods instead.");
        }

        if (!method.DeclaringType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
        {
            throw new ArgumentException(
                $"The type `{method.DeclaringType}` must be derived from the `{type}` type.",
                typeParameterName);
        }

        if (method.ReturnType == typeof(void) && method.GetCustomAttribute<AsyncStateMachineAttribute>() != null)
        {
            throw new NotSupportedException("Async void methods are not supported. Use async Task instead.");
        }

        var parameters = method.GetParameters();

        if (parameters.Length != argumentCount)
        {
            throw new ArgumentException(
                "Argument count must be equal to method parameter count.",
                argumentParameterName);
        }

        foreach (var parameter in parameters)
        {
            if (parameter.IsOut)
            {
                throw new NotSupportedException(
                    "Output parameters are not supported: there is no guarantee that specified method will be invoked inside the same process.");
            }

            if (parameter.ParameterType.IsByRef)
            {
                throw new NotSupportedException(
                    "Parameters, passed by reference, are not supported: there is no guarantee that specified method will be invoked inside the same process.");
            }

            var parameterTypeInfo = parameter.ParameterType.GetTypeInfo();

            if (parameterTypeInfo.IsSubclassOf(typeof(Delegate)) || parameterTypeInfo.IsSubclassOf(typeof(Expression)))
            {
                throw new NotSupportedException(
                    "Anonymous functions, delegates and lambda expressions aren't supported in job method parameters: it's very hard to serialize them and all their scope in general.");
            }
        }
    }

    public static object[] GetExpressionValues(IEnumerable<Expression> expressions)
    {
        return expressions.Select(GetExpressionValue).ToArray();
    }

    public static object GetExpressionValue(Expression expression)
    {
        var constantExpression = expression as ConstantExpression;

        return constantExpression != null
            ? constantExpression.Value
            : CachedExpressionCompiler.Evaluate(expression);
    }
}