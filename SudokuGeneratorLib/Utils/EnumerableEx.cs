using System;
using System.Collections.Generic;

namespace SudokuGeneratorLib.Utils
{
    public static class EnumerableEx
    {
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            IList<TSource> sourceList = source as IList<TSource>;
            if (sourceList != null)
            {
                if (sourceList.Count > 0)
                    return sourceList[0];
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                        return enumerator.Current;
                }
            }

            return defaultValue;
        }
    }
}