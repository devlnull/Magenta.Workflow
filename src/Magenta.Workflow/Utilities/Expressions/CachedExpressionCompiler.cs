using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Hangfire.Common.ExpressionUtil;

namespace Magenta.Workflow.Utilities.Expressions;

[ExcludeFromCodeCoverage]
internal static class CachedExpressionCompiler
{
    public static Func<TModel, TValue> Process<TModel, TValue>(Expression<Func<TModel, TValue>> lambdaExpression)
    {
        return Compiler<TModel, TValue>.Compile(lambdaExpression);
    }

    private static class Compiler<TIn, TOut>
    {
        private static Func<TIn, TOut> _identityFunc;

        private static readonly ConcurrentDictionary<MemberInfo, Func<TIn, TOut>> _simpleMemberAccessDict =
            new ConcurrentDictionary<MemberInfo, Func<TIn, TOut>>();

        private static readonly ConcurrentDictionary<MemberInfo, Func<object, TOut>> _constMemberAccessDict =
            new ConcurrentDictionary<MemberInfo, Func<object, TOut>>();

        private static readonly ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>>
            _fingerprintedCache =
                new ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>>();

        public static Func<TIn, TOut> Compile(Expression<Func<TIn, TOut>> expr)
        {
            return CompileFromIdentityFunc(expr)
                   ?? CompileFromConstLookup(expr)
                   ?? CompileFromMemberAccess(expr)
                   ?? CompileFromFingerprint(expr)
                   ?? CompileSlow(expr);
        }

        private static Func<TIn, TOut> CompileFromConstLookup(Expression<Func<TIn, TOut>> expr)
        {
            ConstantExpression constExpr = expr.Body as ConstantExpression;
            if (constExpr != null)
            {
                TOut constantValue = (TOut) constExpr.Value;
                return _ => constantValue;
            }

            return null;
        }

        private static Func<TIn, TOut> CompileFromIdentityFunc(Expression<Func<TIn, TOut>> expr)
        {
            if (expr.Body == expr.Parameters[0])
            {
                return _identityFunc ?? (_identityFunc = expr.Compile());
            }

            return null;
        }

        private static Func<TIn, TOut> CompileFromFingerprint(Expression<Func<TIn, TOut>> expr)
        {
            List<object> capturedConstants;
            ExpressionFingerprintChain fingerprint =
                FingerprintingExpressionVisitor.GetFingerprintChain(expr, out capturedConstants);

            if (fingerprint != null)
            {
                var del = _fingerprintedCache.GetOrAdd(fingerprint, _ =>
                {
                    var hoistedExpr = HoistingExpressionVisitor<TIn, TOut>.Hoist(expr);
                    return hoistedExpr.Compile();
                });
                return model => del(model, capturedConstants);
            }

            return null;
        }

        private static Func<TIn, TOut> CompileFromMemberAccess(Expression<Func<TIn, TOut>> expr)
        {
            MemberExpression memberExpr = expr.Body as MemberExpression;
            if (memberExpr != null)
            {
                if (memberExpr.Expression == expr.Parameters[0] || memberExpr.Expression == null)
                {
                    return _simpleMemberAccessDict.GetOrAdd(memberExpr.Member, _ => expr.Compile());
                }

                ConstantExpression constExpr = memberExpr.Expression as ConstantExpression;
                if (constExpr != null)
                {
                    var del = _constMemberAccessDict.GetOrAdd(memberExpr.Member, _ =>
                    {
                        var constParamExpr = Expression.Parameter(typeof(object), "capturedLocal");
                        var constCastExpr = Expression.Convert(constParamExpr, memberExpr.Member.DeclaringType);
                        var newMemberAccessExpr = memberExpr.Update(constCastExpr);
                        var newLambdaExpr = Expression.Lambda<Func<object, TOut>>(newMemberAccessExpr, constParamExpr);
                        return newLambdaExpr.Compile();
                    });

                    object capturedLocal = constExpr.Value;
                    return _ => del(capturedLocal);
                }
            }

            return null;
        }

        private static Func<TIn, TOut> CompileSlow(Expression<Func<TIn, TOut>> expr)
        {
            return expr.Compile();
        }
    }
}