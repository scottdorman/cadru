//------------------------------------------------------------------------------
// <copyright file="ExecutionEnvironment.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using Polly;

namespace Cadru.Polly
{
    /// <summary>
    /// Represents both a <see cref="Context" /> and an <see cref="IAsyncPolicy" /> for execution.
    /// </summary>
    public class AsyncExecutionEnvironment : PolicyExecutionEnvironment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionEnvironment" />.
        /// </summary>
        /// <param name="policy">The policy for executing actions.</param>
        /// <param name="context">The context for a single execution through a Policy.</param>
        public AsyncExecutionEnvironment(IAsyncPolicy policy, Context context) : base(context)
        {
            this.Policy = policy;
        }

        /// <summary>
        /// The asynchronous policy for execution.
        /// </summary>
        public IAsyncPolicy Policy { get; }
    }

    /// <summary>
    /// Represents both a <see cref="Context" /> and an <see cref="ISyncPolicy" /> for execution.
    /// </summary>
    public class ExecutionEnvironment : PolicyExecutionEnvironment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionEnvironment" />.
        /// </summary>
        /// <param name="policy">The policy for executing actions.</param>
        /// <param name="context">The context for a single execution through a Policy.</param>
        public ExecutionEnvironment(ISyncPolicy policy, Context context) : base(context)
        {
            this.Policy = policy;
        }

        /// <summary>
        /// The synchronous policy for execution.
        /// </summary>
        public ISyncPolicy Policy { get; }
    }

    /// <summary>
    /// Represents both a <see cref="Context" /> and a Policy for execution.
    /// </summary>
    public abstract class PolicyExecutionEnvironment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyExecutionEnvironment" />.
        /// </summary>
        /// <param name="context">The context for a single execution through a Policy.</param>
        protected PolicyExecutionEnvironment(Context context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Context for a single execution through a Policy.
        /// </summary>
        public Context Context { get; }
    }
}