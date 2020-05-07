using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public static partial class Utility
    {
        public static class Assembly
        {
            private static readonly System.Reflection.Assembly[] s_Assemblies = null;
            private static readonly List<Type> s_AssemblyTypes = new List<Type>();

            static Assembly()
            {
                s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            public static Type[] GetTypes()
            {
                if (s_AssemblyTypes.Count <= 0)
                {
                    foreach (System.Reflection.Assembly assembly in s_Assemblies)
                    {
                        s_AssemblyTypes.AddRange(assembly.GetTypes());
                    }

                    return s_AssemblyTypes.ToArray();
                }
                else {

                    return s_AssemblyTypes.ToArray();
                }

                
            }

            public static List<Type> GetSubTypes(Type type, bool allowGenericSubType)
            {
                var result = GetTypes()
                    .Where(t =>
                        type.IsAssignableFromEx(t) &&
                        ((!allowGenericSubType && !t.IsGenericType) || allowGenericSubType)
                        );

                return result.ToList();

            }

        }
    }


}
