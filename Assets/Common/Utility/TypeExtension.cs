using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TypeExtension
{
    //扩展判断泛型的继承
    public static bool IsAssignableFromEx(this Type parentType, Type subType)
    {
        if (!parentType.IsGenericType)
        {
            return parentType.IsAssignableFrom(subType);
        }
        else
        {
            var interfaceTypes = subType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == parentType)
                    return true;
            }

            if (subType.IsGenericType && subType.GetGenericTypeDefinition() == parentType)
                return true;

            Type baseType = subType.BaseType;
            if (baseType == null) return false;

            return IsAssignableFromEx(parentType, baseType);
        }
    }

    public static bool IsType(this Type type, List<Type> types)
    {
        foreach (Type t in types)
        {
            if (type == t)
            {
                return true;
            }
        }

        return false;
    }
}
