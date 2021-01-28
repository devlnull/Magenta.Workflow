using System;

namespace Magenta.Workflow.Context.Base
{
    public class FlowEntity
    {
        public long Id { get; set; }
        public Guid GuidRow { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; }

        static internal TEntity InitializeType<TEntity>(TEntity entity)
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
