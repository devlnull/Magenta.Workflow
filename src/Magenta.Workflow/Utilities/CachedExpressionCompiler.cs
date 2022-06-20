using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Magenta.Workflow.Utilities;

[ExcludeFromCodeCoverage]
internal static class CachedExpressionCompiler
{
    private static readonly ParameterExpression UnusedParameterExpr = Expression.Parameter(typeof(object), "_unused");

    public static object Evaluate(Expression arg)
    {
        if (arg == null)
        {
            throw new ArgumentNullException(nameof(arg));
        }

        Func<object, object> func = Wrap(arg);
        return func(null);
    }

    private static Func<object, object> Wrap(Expression arg)
    {
        var lambdaExpr = Expression.Lambda<Func<object, object>>(Expression.Convert(arg, typeof(object)), UnusedParameterExpr);
        return Expressions.CachedExpressionCompiler.Process(lambdaExpr);
    }
}