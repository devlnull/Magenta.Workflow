using System;
using System.Collections.Generic;
using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Managers.States
{
    public class InMemoryStateManager : IStateManager
    {
        public Dictionary<Type, object> RepoDict = new Dictionary<Type, object>();
        private readonly object _locker = new object();
        public IFlowSet<TEntity> GetFlowSet<TEntity>()
            where TEntity : FlowEntity
        {
            lock (_locker)
            {
                RepoDict ??= new Dictionary<Type, object>();
                var setType = typeof(IFlowSet<TEntity>);
                if (RepoDict.TryGetValue(setType, out var repo))
                    return repo as InMemoryFlowSet<TEntity>;

                var repoAsFlowSet = new InMemoryFlowSet<TEntity>();
                RepoDict.Add(setType, repoAsFlowSet);
                return repoAsFlowSet;
            }
        }
    }
}
