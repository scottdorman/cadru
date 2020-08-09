using System;

namespace Cadru
{
    public sealed class EmptyDisposable : IDisposable
    {
        /// <summary>
        /// Singleton default disposable.
        /// </summary>
        public static readonly EmptyDisposable Instance = new EmptyDisposable();

        private EmptyDisposable()
        {
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Dispose()
        {
            // no op
        }
    }
}
