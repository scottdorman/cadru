
using Polly;

namespace Cadru.Data.Dapper
{
    public abstract class PollyExecutionContext
    {
        protected PollyExecutionContext(Context context)
        {
            this.Context = context;
        }

        public Context Context { get; }
    }

    public class ExecutionContext : PollyExecutionContext
    {
        internal ExecutionContext(ISyncPolicy policy, Context context) : base(context)
        {
            this.Policy = policy;
        }

        public ISyncPolicy Policy { get; }
    }

    public class AsyncExecutionContext : PollyExecutionContext
    {
        internal AsyncExecutionContext(IAsyncPolicy policy, Context context) : base(context)
        {
            this.Policy = policy;
        }

        public IAsyncPolicy Policy { get; }
    }
}
