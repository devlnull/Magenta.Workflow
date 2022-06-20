using System.Collections.Generic;

namespace Magenta.Workflow.Utilities.Expressions;

internal delegate TValue Hoisted<in TModel, out TValue>(TModel model, List<object> capturedConstants);