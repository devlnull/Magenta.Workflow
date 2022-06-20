﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Magenta.Workflow.Utilities.Expressions;

[ExcludeFromCodeCoverage]
internal abstract class ExpressionFingerprint
{
    protected ExpressionFingerprint(ExpressionType nodeType, Type type)
    {
        NodeType = nodeType;
        Type = type;
    }

    // the type of expression node, e.g. OP_ADD, MEMBER_ACCESS, etc.
    public ExpressionType NodeType { get; }

    // the CLR type resulting from this expression, e.g. int, string, etc.
    public Type Type { get; }

    internal virtual void AddToHashCodeCombiner(HashCodeCombiner combiner)
    {
        combiner.AddInt32((int) NodeType);
        combiner.AddObject(Type);
    }

    protected bool Equals(ExpressionFingerprint other)
    {
        return (other != null)
               && (NodeType == other.NodeType)
               && Type == other.Type;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ExpressionFingerprint);
    }

    public override int GetHashCode()
    {
        HashCodeCombiner combiner = new HashCodeCombiner();
        AddToHashCodeCombiner(combiner);
        return combiner.CombinedHash;
    }
}