using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace StarForce
{
    public class BaseUIItemWrapper<TItem, TStyle, TType> : MonoBehaviour where TItem : BaseItem
    {
        protected TItem m_UIItem;

        private bool m_LoadingUIItem;

        public static List<TaskCompletionSource<TItem>> UIItemTcsList = new List<TaskCompletionSource<TItem>>();

        [SerializeField]
        protected TStyle m_Style;

        [SerializeField]
        protected TType m_Type;

        public TStyle Style { get => m_Style; set => m_Style = value; }
        public TType Type { get => m_Type; set => m_Type = value; }

        private void Awake()
        {
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            m_UIItem = null;
        }

        private async void ShowUIItem(int typeID, string path)
        {
            await ShowUIItemAsync(typeID, path);
        }

        private async Task<TItem> ShowUIItemAsync(int typeID, string path)
        {
            m_LoadingUIItem = true;

            m_UIItem = await GameEntry.Entity.ShowUIItemAsync<TItem>(typeID, this.transform, path);

            m_LoadingUIItem = false;

            foreach (TaskCompletionSource<TItem> loadUIItemTcs in UIItemTcsList)
            {
                loadUIItemTcs.SetResult(m_UIItem);
            }
            UIItemTcsList.Clear();

            return m_UIItem;
        }

        public async Task<TItem> GetUIItem(int typeID, string path)
        {
            if (m_LoadingUIItem)
            {
                TaskCompletionSource<TItem> loadUIItemTcs = new TaskCompletionSource<TItem>();
                UIItemTcsList.Add(loadUIItemTcs);

                m_UIItem = await loadUIItemTcs.Task;

                return m_UIItem;
            }

            if (m_UIItem == null)
            {
                m_UIItem = await ShowUIItemAsync(typeID, path);
                return m_UIItem;
            }
            else
            {
                return m_UIItem;
            }


        }

#if UNITY_EDITOR
        [Button("Reshow UIItem")]
        public void ReshowItem()
        {
            RemoveItem();
            UIItemUtility.AddUIItem<TItem, TStyle, TType>(gameObject);
        }

        [Button("Remove UIItem")]
        protected void RemoveItem()
        {
            UIItemUtility.RemoveUIItems(gameObject);

            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
#endif

    }

}


