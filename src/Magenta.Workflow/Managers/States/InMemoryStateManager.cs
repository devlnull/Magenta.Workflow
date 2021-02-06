using System;
using System.Collections.Generic;
using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Managers.States
{
    public class InMemoryStateManager : IStateManager
    {
        private static readonly object locker = new object();
        public static Dictionary<Type, object> RepoDict  = new Dictionary<Type, object>();

        public IFlowSet<TEntity> GetFlowSet<TEntity>()
            where TEntity : FlowEntity
        {
            lock (locker)
            {
                if (RepoDict == null) RepoDict = new Dictionary<Type, object>();
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
