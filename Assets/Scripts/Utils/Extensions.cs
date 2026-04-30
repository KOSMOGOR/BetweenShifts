using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class ListExtensions {
    public static T RandomElement<T>(this IEnumerable<T> enumerable) {
        return enumerable.ElementAt(Random.Range(0, enumerable.Count() - 1));
    }
}