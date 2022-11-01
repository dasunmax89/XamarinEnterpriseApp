using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamarinEnterpriseApp.Xamarin.Core.Extensions
{
    public static class ObservableExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            if (source != null)
            {
                foreach (T item in source)
                {
                    collection.Add(item);
                }
            }

            return collection;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == 0;
        }

        public static T GetDefaultItem<T>(this IEnumerable<T> source)
        {
            T item = default(T);

            if (source != null && source.Count() == 1)
            {
                item = source.FirstOrDefault();
            }

            return item;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
