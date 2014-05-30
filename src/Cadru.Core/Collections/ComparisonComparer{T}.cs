using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadru.Collections
{
    public class ComparisonComparer<T> : Comparer<T>
    {
        private readonly Comparison<T> _comparison;
 
        protected ComparisonComparer(Comparison<T> comparison) {
            _comparison = comparison;
        }

        public static Comparer<T> Create(Comparison<T> comparison)
        {
            if (comparison == null)
                throw new ArgumentNullException("comparison");

            return new ComparisonComparer<T>(comparison);
        }

        public override int Compare(T x, T y) {
            return _comparison(x, y);
        }
    }
}
