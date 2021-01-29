namespace Magenta.Workflow.Context.Structures
{
    public enum FlowStateTypes : byte
    {
        Purposed = 1,
        Active = 2,
        Request = 3,
        Close = 4,
        Suspend = 5,
        Expired = 6,
        Other = 255,
    }
}
