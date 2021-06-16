using System;
using System.Linq;
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
                    Id = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    ModifiedAt = DateTime.UtcNow.AddDays(-1),
                    Deleted = false,
                    Name = "Hire",
                },
                new FlowType()
                {
                    Id = Guid.Parse("2b013ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    ModifiedAt = DateTime.UtcNow.AddDays(-1),
                    Deleted = false,
                    Name = "Hire1",
                },
            };
        }

        public static FlowState[] GetFlowStates()
        {
            return new FlowState[]
            {
                new FlowState()
                {
                    Id = Guid.Parse("1b016ce2-9625-17ca-9f3a-e2b239d8e1d5"),
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
                    Id = Guid.Parse("2b016ce1-9625-17ca-9f2a-e2b239d8e1d1"),
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
                    Id = Guid.Parse("3b016ce1-5225-35ca-9f2a-e2b239d8e1d2"),
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
                    Id = Guid.Parse("4b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
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
                    Id = Guid.Parse("5b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
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
                    Id = Guid.Parse("6b021ce1-6665-42ca-9f2a-e2b215d8e1d2"),
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
                //States
                //1b016ce2-9625-17ca-9f3a-e2b239d8e1d5-JobSeekerRequest
                //2b016ce1-9625-17ca-9f2a-e2b239d8e1d1-Close
                //3b016ce1-5225-35ca-9f2a-e2b239d8e1d2-Review
                //4b021ce1-5225-42ca-9f2a-e2b215d8e1d2-Approve1
                //5b021ce1-5225-42ca-9f2a-e2b215d8e1d2-Approve2
                //6b021ce1-6665-42ca-9f2a-e2b215d8e1d2-Active

                #region Start
                new FlowTransition()
                {
                    Id = Guid.Parse("0b011ce1-9625-47ca-9f3a-e3b239d8e1d5"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Start",
                    Title = "Start",
                    TransitionType = FlowTransitionTypes.Start,
                    SourceId = null,
                    DestinationId = Guid.Parse("1b016ce2-9625-17ca-9f3a-e2b239d8e1d5"),
                },
                #endregion Start
                #region JobSeekerRequest 
                new FlowTransition()
                {
                    Id = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = Guid.Parse("1b016ce2-9625-17ca-9f3a-e2b239d8e1d5"),
                    DestinationId = Guid.Parse("3b016ce1-5225-35ca-9f2a-e2b239d8e1d2"),
                },
                new FlowTransition()
                {
                    Id = Guid.Parse("2b016ce2-9625-47ca-9f3a-e2b239d8e1d1"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = Guid.Parse("1b016ce2-9625-17ca-9f3a-e2b239d8e1d5"),
                    DestinationId = Guid.Parse("2b016ce1-9625-17ca-9f2a-e2b239d8e1d1"),
                },
                #endregion JobSeekerRequest
                #region Review 
                new FlowTransition()
                {
                    Id = Guid.Parse("3b067ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = Guid.Parse("3b016ce1-5225-35ca-9f2a-e2b239d8e1d2"),
                    DestinationId = Guid.Parse("4b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                },
                new FlowTransition()
                {
                    Id = Guid.Parse("4b015ce2-9625-47ca-9f3a-e2b449d8e1d1"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = Guid.Parse("3b016ce1-5225-35ca-9f2a-e2b239d8e1d2"),
                    DestinationId = Guid.Parse("2b016ce1-9625-17ca-9f2a-e2b239d8e1d1"),
                },
                #endregion Review
                #region Approve1 
                new FlowTransition()
                {
                    Id = Guid.Parse("5b068ce2-9625-47ca-9f2a-e3b239d8e1d5"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = Guid.Parse("4b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                    DestinationId = Guid.Parse("5b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                },
                new FlowTransition()
                {
                    Id = Guid.Parse("6b015ce1-9625-47ca-9f3a-e2b666d8e1d1"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = Guid.Parse("4b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                    DestinationId = Guid.Parse("2b016ce1-9625-17ca-9f2a-e2b239d8e1d1"),
                },
                #endregion Approve1
                #region Approve2 
                new FlowTransition()
                {
                    Id = Guid.Parse("7b068ce2-6547-47ca-9f2a-e3b239d8e1d5"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Accept",
                    Title = "Accept",
                    TransitionType = FlowTransitionTypes.Accept,
                    SourceId = Guid.Parse("5b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                    DestinationId = Guid.Parse("6b021ce1-6665-42ca-9f2a-e2b215d8e1d2"),
                },
                new FlowTransition()
                {
                    Id = Guid.Parse("8b015ce1-9157-47ca-9f3a-e2b666d8e1d1"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = Guid.Parse("5b021ce1-5225-42ca-9f2a-e2b215d8e1d2"),
                    DestinationId = Guid.Parse("2b016ce1-9625-17ca-9f2a-e2b239d8e1d1"),
                },
                #endregion Approve2
                #region Active 
                new FlowTransition()
                {
                    Id = Guid.Parse("9b650ce1-9117-47ca-1f3a-e2b666d8e1d2"),
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5"),
                    CreatedAt = DateTime.UtcNow.AddMinutes(-59),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(-59),
                    Deleted = false,
                    Name = "Reject",
                    Title = "Reject",
                    TransitionType = FlowTransitionTypes.Reject,
                    SourceId = Guid.Parse("6b021ce1-6665-42ca-9f2a-e2b215d8e1d2"),
                    DestinationId = Guid.Parse("2b016ce1-9625-17ca-9f2a-e2b239d8e1d1"),
                },
                #endregion Active
            };
        }

        public static FlowInstance[] GetFlowInstances()
        {
            return new FlowInstance[]
            {
                new FlowInstance()
                {
                    Id = Guid.Parse("2b020ce2-9625-47ca-9f3a-e3b239d8e1d5"),
                    AccessPhrase = "secure",
                    Active = true,
                    CreatedAt = DateTime.UtcNow.AddMinutes(10),
                    ModifiedAt = DateTime.UtcNow.AddMinutes(10),
                    Deleted = false,
                    InitializerId = "1",
                    Payload = string.Empty,
                    Title = "Hire devlnull request",
                    TypeId = Guid.Parse("1b016ce9-9625-47ca-9f3a-e3b239d8e1d5")
                }
            };
        }

        public static FlowStep[] GetFlowSteps()
        {
            return new FlowStep[]
            {
                new FlowStep()
                {
                    Id = Guid.Parse("3b030ce3-9625-47ca-9f3a-e3b239d8e1d5"),
                    InstanceId = Guid.Parse("2b020ce2-9625-47ca-9f3a-e3b239d8e1d5"),
                    Comment = "Initialization request",
                    CreatedAt = DateTime.UtcNow.AddHours(-100),
                    ModifiedAt = DateTime.UtcNow.AddHours(100),
                    Payload = string.Empty,
                    Deleted = false,
                    IdentityId = "1",
                    IsCurrent = true,
                    TransitionId = Guid.Parse("0b011ce1-9625-47ca-9f3a-e3b239d8e1d5")
                }
            };
        }
    }
}
