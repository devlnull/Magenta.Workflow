using System;
using Magenta.Workflow.Context.Flows;

namespace Magenta.Workflow.Tests.Mock
{
    public static class MockData
    {
        public static FlowType[] GetFlowTypes()
        {
            return new FlowType[]
            {
                new FlowType()
                {
                    Id = 999999,
                    GuidRow = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    ModifiedAt = DateTime.UtcNow.AddDays(-1),
                    Deleted = false,
                    EntityPayloadType = typeof(MockState).Name,
                    EntityType = typeof(MockState).Name,
                    Name = "Hire",
                },
            };
        }
    }
}
