using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static void ForEach<T>(this List<T> list, Action<T> action)
    {
        foreach (T item in list) {

            action.Invoke(item);
        }

    }

    public static void ForEachLog(this List<string> list)
    {
        list.ForEach((item) =>
        {
            Debug.Log(item);
        });

    }

    public static List<string> GetTypeNames(this List<Type> list)
    {
        List<string> typeNames = new List<string>();

        list.ForEach((type) =>
        {
            typeNames.Add(type.FullName);
        });

        return typeNames;
    }


}




