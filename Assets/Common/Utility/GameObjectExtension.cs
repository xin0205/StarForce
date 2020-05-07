using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static void ForEach(this Transform transform, Action<Transform> action)
    {
        foreach (Transform child in transform)
        {
            action.Invoke(child);
            if (child != null)
            {
                child.ForEach(action);
            }
        }
    }

    public static void ForEach(this GameObject go, Action<GameObject> action)
    {
        foreach (Transform child in go.transform)
        {
            action.Invoke(child.gameObject);
            if (child != null)
            {
                child.gameObject.ForEach(action);
            }
        }
    }

    public static void ForEachComponent<T>(this GameObject go, Action<T> action)
    {
        foreach (T t in go.GetComponents<T>())
        {
            action.Invoke(t);
        }
    }

    public static void ForEachComponent(this GameObject go, Predicate<Component> match, Action<Component> action)
    {
        foreach (Component t in go.GetComponents<Component>())
        {
            if (match(t))
                action.Invoke(t);
        }

    }

    public static bool HasComponent(this GameObject go, List<Type> types)
    {
        bool isType = false;
        go.ForEachComponent<Component>((component) =>
        {
            if (component.GetType().IsType(types))
            {
                isType = true;
                return;
            }
        });

        return isType;

    }

    public static void RemoveComponent(this GameObject go, Predicate<Component> match)
    {
        List<Component> removeComponents = new List<Component>();

        foreach (var component in go.GetComponents<Component>())
        {
            if (match(component))
                removeComponents.Add(component);
        }

        for (int i = removeComponents.Count - 1; i >= 0; i--)
        {
            UnityEngine.Object.DestroyImmediate(removeComponents[i], true);
        }

    }

    public static void RemoveChilds(this GameObject go, Predicate<GameObject> match)
    {
        List<GameObject> deleteChilds = new List<GameObject>();

        go.ForEach((child) =>
        {
            if (match(child))
                deleteChilds.Add(child);

        });

        for (int i = deleteChilds.Count - 1; i >= 0; i--)
        {
            try
            {
                //子物体是预制体，此预制体的子物体无法删除
                GameObject.DestroyImmediate(deleteChilds[i].gameObject);
            }
            catch
            {

            }
        }
    }
}

