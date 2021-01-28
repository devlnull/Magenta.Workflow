using System;
using System.Collections.Generic;
using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Managers.States
{
    public class InMemoryStateManager : IStateManager
    {
        public static Dictionary<Type, object> RepoDict
            = new Dictionary<Type, object>();

        public IFlowSet<TEntity> GetFlowSet<TEntity>()
            where TEntity : FlowEntity
        {
            RepoDict.TryGetValue(typeof(TEntity), out object repo);
            var repoAsFlowSet = repo as FlowSet<TEntity>;
            if (repoAsFlowSet == null)
            {
                repoAsFlowSet = new FlowSet<TEntity>();
                RepoDict[typeof(TEntity)] = repoAsFlowSet;
            }
            return repoAsFlowSet;
        }
    }
}
