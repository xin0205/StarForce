using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace StarForce.Editor
{
    public static class UIItemEditor
    {
        [MenuItem("GameObject/UIItem/Add UIItems", false, 1)]
        static void AddUIItems(MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;

            parent.transform.ForEach((child) =>
            {
                child.gameObject.ForEachComponent((component) => UIItemUtility.IsUIItemWrapperComponent(component), (component) =>
                {
                    Type wrapperType = component.GetType();

                    MethodInfo methodInfo = wrapperType.GetMethod("ReshowItem");
                    methodInfo.Invoke(component, null);
                });

            });

        }


        [MenuItem("GameObject/UIItem/Remove UIItems", false, 1)]
        public static void RemoveUIItems(MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;

            UIItemUtility.RemoveUIItems(parent);

            EditorSceneManager.MarkSceneDirty(parent.gameObject.scene);


        }

        [MenuItem("GameObject/UIItem/UIItem Wrapper", false, 1)]
        static void AddUIItemWrapper(MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;

            UIItemUtility.AddUIItemWrapper(parent);

        }


    }

}

