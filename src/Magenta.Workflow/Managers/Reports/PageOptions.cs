using System;

namespace Magenta.Workflow.Managers.Reports
{
    public class PageOptions
    {
        public uint? Offset { get; set; }
        public uint? Limit { get; set; }

        internal int? GetOffset()
        {
            if (Offset.HasValue == false) return null;
            var val = Convert.ToInt32(Offset);
            return val;
        }

        internal int? GetLimit()
        {
            if (Limit.HasValue == false) return null;
            var val = Convert.ToInt32(Limit);
            return val;
        }
    }
}
