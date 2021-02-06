using System;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Context.Structures;

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
                    EntityPayloadType = nameof(MockState),
                    EntityType = nameof(MockState),
                    Name = "Hire",
                },
            };
        }

        public static FlowState[] GetFlowStates()
        {
            return new FlowState[]
            {
                new FlowState()
                {
                    Id = 999900,
                    GuidRow = Guid.Parse("1b016ce2-9625-17ca-9f3a-e2b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddHours(-10),
                    ModifiedAt = DateTime.UtcNow.AddDays(-10),
                    Deleted = false,
                    Name = "JobSeekerRequest",
                    Title = "Job Seeker Request",
                    TypeId = GetFlowTypes()[0].Id,
                    StateType = (byte) FlowStateTypes.Purposed,
                },
                new FlowState()
                {
                    Id = 999901,
                    GuidRow = Guid.Parse("2b016ce1-9625-17ca-9f2a-e2b239d8e1d1"),
                    CreatedAt = DateTime.UtcNow.AddHours(-9),
                    ModifiedAt = DateTime.UtcNow.AddDays(-9),
                    Deleted = false,
                    Name = "Close",
                    Title = "Closed",
                    TypeId = GetFlowTypes()[0].Id,
                    StateType = (byte) FlowStateTypes.Close,
                },
                new FlowState()
                {
                    Id = 999902,
                    GuidRow = Guid.Parse("7b016ce1-5225-35ca-9f2a-e2b239d8e1d2"),
                    CreatedAt = DateTime.UtcNow.AddHours(-8),
                    ModifiedAt = DateTime.UtcNow.AddDays(-8),
                    Deleted = false,
                    Name = "Review",
                    Title = "Reviewed By HR",
                    TypeId = GetFlowTypes()[0].Id,
                    StateType = (byte) FlowStateTypes.Purposed,
                },
                new FlowState()
                {
                    Id = 999903,
                    GuidRow = Guid.Parse("7b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                    CreatedAt = DateTime.UtcNow.AddHours(-7),
                    ModifiedAt = DateTime.UtcNow.AddDays(-7),
                    Deleted = false,
                    Name = "Approve1",
                    Title = "Approve by Technical Manager",
                    TypeId = GetFlowTypes()[0].Id,
                    StateType = (byte) FlowStateTypes.Purposed,
                },
                new FlowState()
                {
                    Id = 999904,
                    GuidRow = Guid.Parse("7b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                    CreatedAt = DateTime.UtcNow.AddHours(-6),
                    ModifiedAt = DateTime.UtcNow.AddDays(-6),
                    Deleted = false,
                    Name = "Approve2",
                    Title = "Approved by CEO",
                    TypeId = GetFlowTypes()[0].Id,
                    StateType = (byte) FlowStateTypes.Purposed,
                },
                new FlowState()
                {
                    Id = 999905,
                    GuidRow = Guid.Parse("7b021ce1-6665-42ca-9f2a-e2b215d8e1d2"),
                    CreatedAt = DateTime.UtcNow.AddHours(-5),
                    ModifiedAt = DateTime.UtcNow.AddDays(-5),
                    Deleted = false,
                    Name = "Active",
                    Title = "Working",
                    TypeId = GetFlowTypes()[0].Id,
                    StateType = (byte) FlowStateTypes.Active,
                },
            };
        }

        public static FlowTransition[] GetFlowTransitions()
        {
            return new FlowTransition[]
            {
                //999900-JobSeekerRequest
                //999901-Close
                //999902-Review
                //999903-Approve1
                //999904-Approve2
                //999905-Active

                #region JobSeekerRequest 
                new FlowTransition()
                {
                    Id = 999901,
                    GuidRow = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = 999900,
                    DestinationId = 999902,
                },
                new FlowTransition()
                {
                    Id = 999902,
                    GuidRow = Guid.Parse("2b016ce2-9625-47ca-9f3a-e2b239d8e1d1"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = 999900,
                    DestinationId = 999901,
                },
                #endregion JobSeekerRequest
                #region Review 
                new FlowTransition()
                {
                    Id = 999903,
                    GuidRow = Guid.Parse("3b067ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = 999902,
                    DestinationId = 999903,
                },
                new FlowTransition()
                {
                    Id = 999904,
                    GuidRow = Guid.Parse("4b015ce2-9625-47ca-9f3a-e2b449d8e1d1"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = 999902,
                    DestinationId = 999901,
                },
                #endregion Review
                #region Approve1 
                new FlowTransition()
                {
                    Id = 999905,
                    GuidRow = Guid.Parse("5b068ce2-9625-47ca-9f2a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = 999903,
                    DestinationId = 999904,
                },
                new FlowTransition()
                {
                    Id = 999906,
                    GuidRow = Guid.Parse("6b015ce1-9625-47ca-9f3a-e2b666d8e1d1"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = 999903,
                    DestinationId = 999901,
                },
                #endregion Approve1
                #region Approve2 
                new FlowTransition()
                {
                    Id = 999907,
                    GuidRow = Guid.Parse("7b068ce2-6547-47ca-9f2a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = 999904,
                    DestinationId = 999905,
                },
                new FlowTransition()
                {
                    Id = 999908,
                    GuidRow = Guid.Parse("8b015ce1-9157-47ca-9f3a-e2b666d8e1d1"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = 999904,
                    DestinationId = 999901,
                },
                #endregion Approve2
                #region Active 
                new FlowTransition()
                {
                    Id = 999909,
                    GuidRow = Guid.Parse("9b650ce1-9117-47ca-1f3a-e2b666d8e1d2"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = 999905,
                    DestinationId = 999901,
                },
                #endregion Active
            };
        }
    }
}
