using System;
using System.Collections.Generic;
using System.Text;

namespace RAML.APITools.Tools.Pluralization
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var t in ts)
            {
                action(t);
            }
        }

    }
}
