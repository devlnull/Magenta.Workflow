using System;

namespace Magenta.Workflow.Utilities
{
    public static class ValidatorHelper
    {
        public static bool GuidIsEmpty(this Guid arg)
        {
            if (arg == default) return true;
            if (arg.Equals(new Guid())) return true;

            return false;
        }

        public static bool StringIsEmpty(this string arg)
        {
            if (string.IsNullOrEmpty(arg)) return true;
            if (string.IsNullOrWhiteSpace(arg)) return true;

            return false;
        }
    }
}
