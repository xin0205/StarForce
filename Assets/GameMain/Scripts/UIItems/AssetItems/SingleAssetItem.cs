using UnityEngine;
using UnityEngine.UI;

namespace StarForce
{
    public abstract class SingleAssetItem<T> : BaseItem
    {
        [SerializeField]
        protected SingleAssetItemData<T> m_ItemData = new SingleAssetItemData<T>();

        [SerializeField]
        private Image m_Img;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
        }

        public void SetItem(T itemType)
        {
            m_ItemData.ItemType = itemType;
            SetItem();
        }

        private void SetItem()
        {
            m_Img.sprite = m_ItemData.Asset.GetSprite(m_ItemData.ItemType);
            m_Img.SetNativeSize();
        }
    }
}
