using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace StarForce
{
    public class SingleAssetItemData<T> : BaseItemData
    {
        [SerializeField]
        private UIItemAsset<T> m_Asset;

        [SerializeField]
        private T m_ItemType;

        public T ItemType { get => m_ItemType; set => m_ItemType = value; }
        public UIItemAsset<T> Asset { get => m_Asset; set => m_Asset = value; }

        public SingleAssetItemData(T itemType)
        {
            m_ItemType = itemType;
        }

        public SingleAssetItemData()
        {
        }

        public static SingleAssetItemData<T> Create(T itemType)
        {
            return new SingleAssetItemData<T>(itemType);
        }

    }
}
