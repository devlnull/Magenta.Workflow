using System.Linq;
using System.Reflection;

namespace Magenta.Workflow.Utilities
{
    internal static class MethodInfoExtensions
    {
        public static string GetNormalizedName(this MethodInfo methodInfo)
        {
            return methodInfo.Name.Contains(".") && methodInfo.IsFinal && methodInfo.IsPrivate
                ? methodInfo.Name.Split('.').Last()
                : methodInfo.Name;
        }
    }
}