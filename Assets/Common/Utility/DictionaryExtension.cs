using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExtension
{
    public static void Foreach<T1, T2>(this Dictionary<T1, T2> dict, Action<T1, T2> action) {

        foreach (KeyValuePair<T1, T2> kv in dict) {

            action(kv.Key, kv.Value);

        }
    }

    public static void TryAdd<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 value)
    {
        if (!dict.ContainsKey(key)) {
            dict.Add(key, value);
        }
    }
}
