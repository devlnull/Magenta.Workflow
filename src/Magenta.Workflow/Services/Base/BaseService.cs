using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Entities.Base;
using Magenta.Workflow.Managers.States.Abstracts;
using Magenta.Workflow.Managers.States.Concrete;
using System;

namespace Magenta.Workflow.Services.Base
{
    public class BaseService
    {
        internal readonly IStateManager _stateManager;
        public BaseService()
        {
            _stateManager = new StateManagerFactory().CreateInstance() ??
                throw new FlowException(nameof(StateManagerFactory));
        }


        internal TEntity InitializeType<TEntity>(TEntity entity)
            where TEntity : FlowEntity
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.ModifiedAt = DateTime.UtcNow;
            entity.GuidRow = Guid.NewGuid();
            entity.Deleted = false;
            return entity;
        }

    }
}
