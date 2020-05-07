using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace StarForce
{
    public class MultiAssetsItemData<T1, T2> : BaseItemData
    {
        [SerializeField]
        private Dictionary<T1, UIItemAsset<T2>> m_Assets = new Dictionary<T1, UIItemAsset<T2>>();

        [SerializeField]
        private T1 m_ItemStyle = default;

        [SerializeField]
        private T2 m_ItemType = default;

        public T1 ItemStyle { get => m_ItemStyle; set => m_ItemStyle = value; }
        public T2 ItemType { get => m_ItemType; set => m_ItemType = value; }
        public Dictionary<T1, UIItemAsset<T2>> Assets { get => m_Assets; set => m_Assets = value; }

        public MultiAssetsItemData(T1 itemStyle, T2 itemType)
        {
            m_ItemStyle = itemStyle;
            m_ItemType = itemType;

        }

        public MultiAssetsItemData()
        {
        }

        public static MultiAssetsItemData<T1, T2> Create(T1 itemStyle, T2 itemType)
        {
            return new MultiAssetsItemData<T1, T2>(itemStyle, itemType);
        }

    }
}
