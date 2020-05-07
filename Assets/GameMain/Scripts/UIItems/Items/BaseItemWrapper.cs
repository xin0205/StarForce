using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace StarForce {
    public class BaseItemWrapper<TItem, TType> : BaseUIItemWrapper<TItem, ItemStyle, TType> where TItem : BaseItem where TType : BaseItemData
    {
        private void OnEnable()
        {
            SetItem(m_Type);
        }

        public async Task<TItem> GetUIItem()
        {
             return await GetUIItem((int)m_Style, AssetUtility.GetItemAsset<TItem>());
        }

        public async void SetItem(TType baseItemData) {

            TItem baseItem = await GetUIItem();

            baseItem.SetItem(baseItemData);
        }

        

    }
        

}


