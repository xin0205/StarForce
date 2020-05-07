using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace StarForce {
    public class UIItemWrapper : MonoBehaviour
    {
        private List<string> UIItemTypeNames = UIItemUtility.GetUIItemWrapperTypeNames();

        [ValueDropdown("UIItemTypeNames")]
        public string m_UIItemTypeName;

        [Button("Add UIItem Wrapper")]
        private void AddUIItemWrapper()
        {
            if (string.IsNullOrEmpty(m_UIItemTypeName))
            {
                Debug.LogWarning("m_UIItemTypeName is isNullOrEmpty");
                return;
            }

            RemoveUIItemWrapper();

            System.Type wrapperType = System.Type.GetType(m_UIItemTypeName);

            gameObject.AddComponent(wrapperType);

        }

        [Button("Remove UIItem Wrapper")]
        private void RemoveUIItemWrapper()
        {
            UIItemUtility.RemoveUIItems(gameObject);

            gameObject.RemoveComponent((component) =>
            {
                return UIItemUtility.IsUIItemWrapperComponent(component);

            });

            EditorSceneManager.MarkSceneDirty(gameObject.scene);

        }
    }


}
