using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace StarForce
{
    public abstract class MultiAssetsItem<T1, T2> : BaseItem
    {

        [SerializeField]
        private MultiAssetsItemData<T1, T2> m_ItemData = new MultiAssetsItemData<T1, T2>();


        [SerializeField]
        private Image m_Img;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
        }

        public void SetItem(T1 itemStyle, T2 itemType)
        {
            m_ItemData.ItemStyle = itemStyle;
            m_ItemData.ItemType = itemType;

            SetItem();
        }


        private void SetItem()
        {
            if (!m_ItemData.Assets.ContainsKey(m_ItemData.ItemStyle))
            {
                Debug.LogWarning("m_Assets without " + m_ItemData.ItemStyle);
                return;
            }

            m_Img.sprite = m_ItemData.Assets[m_ItemData.ItemStyle].GetSprite(m_ItemData.ItemType);
            m_Img.SetNativeSize();
        }

    }
}
