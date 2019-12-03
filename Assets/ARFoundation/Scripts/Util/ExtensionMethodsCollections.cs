using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethodsCollections {

	#region Dictionary Utils
	public static int Increment<T>(this Dictionary<T, int> dictionary, T key, int value = 1) {
		int currentCount = 0;
		dictionary.TryGetValue(key, out currentCount);

		currentCount += value;
		dictionary[key] = currentCount;

		return currentCount;
	}
    public static float Increment<T>(this Dictionary<T, float> dictionary, T key, float value = 1) {
        float currentCount = 0;
        dictionary.TryGetValue(key, out currentCount);

        currentCount += value;
        dictionary[key] = currentCount;

        return currentCount;
    }

    public static U GetOrInsertNew<T, U>(this Dictionary<T, U> dictionary, T key) where U : new() {
        if (dictionary.ContainsKey(key)) 
			return dictionary[key];

        U newObject = new U();
        dictionary[key] = newObject;

        return newObject;
    }

	public static U GetOrDefault<T, U>(this Dictionary<T, U> dictionary, T key, U def = default(U)) {
		if (dictionary.ContainsKey(key))
			return dictionary[key];

		return def;
	}
	#endregion

	public static T GetRandomElement<T>(this IList<T> list) {
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static T GetClamped<T>(this IList<T> list, int index) {
		return list[Mathf.Clamp(index, 0, list.Count)];
	}

	public static T GetUnclamped<T>(this IList<T> list, int index) {
		return list[(list.Count + index) % list.Count];
	}

	public static bool IsDefault<T>(this T value) {
		return Equals(value, default(T));
	}

	public static void Shuffle<T>(this IList<T> list) {
		int count = list.Count;

		// Fisher Yates shuffle
		while (count > 1) {
			count--;
			int index = UnityEngine.Random.Range(0, count + 1);
			T value = list[index];

			list[index] = list[count];
			list[count] = value;
		}
	}

	public static bool TrueForAll<T>(this IEnumerable<T> enumerable, Predicate<T> predicate) {
		foreach (var current in enumerable) {
			if (!predicate(current)) {
				return false;
			}
		}
		return true;
	}

	public static bool Exists<T>(this IEnumerable<T> enumerable, Predicate<T> predicate) {
		foreach (var current in enumerable) {
			if (predicate(current)) {
				return true;
			}
		}
		return false;
	}

	public static T Find<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, T def = default(T)) {
		foreach (var current in enumerable) {
			if (predicate(current)) {
				return current;
			}
		}

		return def;
	}

	public static IEnumerable<T> FindAll<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, T def = default(T)) {
		foreach (var current in enumerable) {
			if (predicate(current)) {
				yield return current;
			}
		}
	}

	public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
		foreach (T element in enumerable) {
			action(element);
		}
	}

	public static void ForEachIndex<T>(this IEnumerable<T> enumerable, Action<T, int> action) {
		int index = 0;
		foreach (T element in enumerable) {
			action(element, index++);
		}
	}

	public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T> {
		if (val.CompareTo(min) < 0) 
			return min;
		else if (val.CompareTo(max) > 0) 
			return max;
		else 
			return val;
    }

    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
        return source.MaxBy(selector, null);
    }

    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) {
        if (source == null) throw new ArgumentNullException("source");
        if (selector == null) throw new ArgumentNullException("selector");
        comparer = comparer ?? Comparer<TKey>.Default;

        using (var sourceIterator = source.GetEnumerator()) {
            if (!sourceIterator.MoveNext()) {
                return default(TSource);
                //throw new InvalidOperationException("Sequence contains no elements");
            }
            var max = sourceIterator.Current;
            var maxKey = selector(max);
            while (sourceIterator.MoveNext()) {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, maxKey) > 0) {
                    max = candidate;
                    maxKey = candidateProjected;
                }
            }
            return max;
        }
    }

    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
        return source.MinBy(selector, null);
    }

    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) {
        if (source == null) throw new ArgumentNullException("source");
        if (selector == null) throw new ArgumentNullException("selector");
        comparer = comparer ?? Comparer<TKey>.Default;

        using (var sourceIterator = source.GetEnumerator()) {
            if (!sourceIterator.MoveNext()) {
                return default(TSource);
                //throw new InvalidOperationException("Sequence contains no elements");
            }
            var min = sourceIterator.Current;
            var minKey = selector(min);
            while (sourceIterator.MoveNext()) {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, minKey) < 0) {
                    min = candidate;
                    minKey = candidateProjected;
                }
            }
            return min;
        }
    }
}