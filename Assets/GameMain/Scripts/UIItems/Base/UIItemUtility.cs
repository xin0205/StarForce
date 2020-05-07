using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;
using Common;

namespace StarForce
{
    public static class UIItemUtility
    {
        private static readonly string m_UIItemPrefabRootPath = "Assets/GameMain/UIItems/";
        private static readonly string m_UIItemWrapperPrefabPath = m_UIItemPrefabRootPath + "Base/UIItemWrapper.prefab";

        public static async Task<CurrencyIcon> ShowCurrencyIconAsync(this EntityComponent entityCompoennt, CurrencyStyle currencyStyle, CurrencyType currencyType, Transform parent)
        {
            return await GameEntry.Entity.ShowMultiAssetsItemAsync<CurrencyIcon, CurrencyStyle, CurrencyType>(Constant.UIItem.CurrencyIconTypeID, currencyStyle, currencyType, parent);
        }

        public static async void ShowCurrencyIcon(this EntityComponent entityCompoennt, CurrencyStyle currencyStyle, CurrencyType currencyType, Transform parent)
        {
            await GameEntry.Entity.ShowCurrencyIconAsync(currencyStyle, currencyType, parent);
        }

        public static async Task<RoleIcon> ShowRoleIconAsync(this EntityComponent entityCompoennt, RoleStyle roleStyle, int roleID, Transform parent)
        {
            return await GameEntry.Entity.ShowSingleAssetItemAsync<RoleIcon, int>((int)roleStyle, roleID, parent);
        }

        public static async void ShowRoleIcon(this EntityComponent entityCompoennt, RoleStyle roleStyle, int roleID, Transform parent)
        {
            await GameEntry.Entity.ShowRoleIconAsync(roleStyle, roleID, parent);
        }

        public static async Task<FriendItem> ShowFriendItemAsync(this EntityComponent entityCompoennt, FriendItemData friendItemData, Transform parent)
        {
            return await GameEntry.Entity.ShowItemAsync<FriendItem>(parent, friendItemData);
        }

        public static async void ShowFriendItem(this EntityComponent entityCompoennt, FriendItemData friendItemData, Transform parent)
        {
            await GameEntry.Entity.ShowFriendItemAsync(friendItemData, parent);
        }

        public static async Task<T> ShowMultiAssetsItemAsync<T, T1, T2>(this EntityComponent entityCompoennt, int typeID, T1 currencyStyle, T2 currencyType, Transform parent) where T : MultiAssetsItem<T1, T2>
        {
            T item = await entityCompoennt.ShowUIItemAsync<T>(typeID, parent, AssetUtility.GetAssetItemAsset<T>());

            item.SetItem(currencyStyle, currencyType);

            return item;
        }

        public static async Task<T1> ShowSingleAssetItemAsync<T1, T2>(this EntityComponent entityCompoennt, int typeID, T2 itemType, Transform parent) where T1 : SingleAssetItem<T2>
        {
            T1 item = await entityCompoennt.ShowUIItemAsync<T1>(typeID, parent, AssetUtility.GetAssetItemAsset<T1>());

            item.SetItem(itemType);

            return item;
        }
        public static async Task<T> ShowItemAsync<T>(this EntityComponent entityCompoennt, Transform parent, BaseItemData itemData) where T : BaseItem
        {
            Enum.TryParse(typeof(T).Name, out ItemStyle itemStyle);

            T item = await entityCompoennt.ShowUIItemAsync<T>((int)itemStyle, parent, AssetUtility.GetItemAsset<T>());

            item.SetItem(itemData);

            return item;
        }

        public static async Task<T> ShowUIItemAsync<T>(this EntityComponent entityCompoennt, int typeID, Transform parent, string pathFormat) where T : BaseItem
        {
            int serialId = GameEntry.Entity.GenerateSerialId();
            string entityGroup = typeof(T).Name.Split('_')[0];

            UIItemData uiItemData = UIItemData.Create(serialId, typeID, parent);

            T entity = await entityCompoennt.ShowEntityAsync<T>(entityGroup, Constant.AssetPriority.UIItemAsset, uiItemData, pathFormat);

            return entity;
        }

        public static List<string> GetUIItemWrapperTypeNames()
        {
            return GetUIItemWrapperTypes().GetTypeNames();
        }

        public static void AddUIItem<TUIItem, TStyle, TType>(GameObject wrapperObj) where TUIItem : BaseItem
        {
            BaseUIItemWrapper<TUIItem, TStyle, TType> baseUIItemWrapper = wrapperObj.GetComponent<BaseUIItemWrapper<TUIItem, TStyle, TType>>();

            GameObject uiItemObj = CreatePrefabAtPath(GetAssetItemPath<TUIItem, TStyle, TType>(wrapperObj)); ;
            TUIItem uiItem = uiItemObj.GetComponent<TUIItem>();

            if (IsMultiAssetType(typeof(TUIItem)))
            {
                (uiItem as MultiAssetsItem<TStyle, TType>).SetItem(baseUIItemWrapper.Style, baseUIItemWrapper.Type);

            }
            else if (IsSingleAssetType(typeof(TUIItem)))
            {
                (uiItem as SingleAssetItem<TType>).SetItem(baseUIItemWrapper.Type);

            }
            else if (IsBaseItemType(typeof(TUIItem)))
            {
                uiItem.ReshowItem(baseUIItemWrapper.Type as BaseItemData);

            }

            if (uiItemObj != null)
            {
                GameObjectUtility.SetParentAndAlign(uiItemObj, wrapperObj);
                Undo.RegisterCreatedObjectUndo(uiItemObj, "Create" + uiItemObj.name);
            }

        }

        private static string GetAssetItemPath<TUIItem, TStyle, TType>(GameObject wrapperObj) where TUIItem : BaseItem
        {
            string typeName = typeof(TUIItem).Name;
            string assetSubPath = "AssetItems/" + typeName + "/" + typeName;
            string itemSubPath = "Items/";

            if (IsMultiAssetType(typeof(TUIItem)))
            {
                return m_UIItemPrefabRootPath + assetSubPath + ".prefab";

            }
            else if (IsSingleAssetType(typeof(TUIItem)))
            {
                string styleName = "";

                BaseUIItemWrapper<TUIItem, TStyle, TType> baseUIItemWrapper = wrapperObj.GetComponent<BaseUIItemWrapper<TUIItem, TStyle, TType>>();

                styleName = Enum.GetName(typeof(TStyle), baseUIItemWrapper.Style);

                return string.Format(m_UIItemPrefabRootPath + assetSubPath + "_{0}.prefab", styleName);

            }
            else if (IsBaseItemType(typeof(TUIItem)))
            {
                BaseUIItemWrapper<TUIItem, TStyle, TType> baseUIItemWrapper = wrapperObj.GetComponent<BaseUIItemWrapper<TUIItem, TStyle, TType>>();

                string styleName = Enum.GetName(typeof(TStyle), baseUIItemWrapper.Style);
                return m_UIItemPrefabRootPath + itemSubPath + styleName + ".prefab";

            }


            Debug.Log("Without prefab path:" + typeof(TUIItem).Name);
            return "";

        }

       
        public static List<Type> GetUIItemWrapperTypes()
        {
            return Utility.Assembly.GetSubTypes(typeof(BaseUIItemWrapper<,,>), false);
        }

        public static void RemoveUIItems(GameObject go)
        {
            go.RemoveChilds((child) =>
            {
                Transform parent = child.transform.parent;

                if (parent != null && HasUIItemWrapperComponent(parent.gameObject) &&
                    child.GetComponent<BaseItem>())
                {
                    return true;
                }
                return false;

            });
        }


        public static void AddUIItemWrapper(GameObject parent)
        {
            GameObject wrapperObj = CreatePrefabAtPath(m_UIItemWrapperPrefabPath);

            if (wrapperObj != null)
            {
                GameObjectUtility.SetParentAndAlign(wrapperObj, parent);
                Undo.RegisterCreatedObjectUndo(wrapperObj, "Create" + wrapperObj.name);
            }

        }

        public static bool IsUIItemWrapperComponent(Component component)
        {
            return component.GetType().IsType(GetUIItemWrapperTypes());
        }

        public static bool HasUIItemWrapperComponent(GameObject go)
        {
            return go.HasComponent(GetUIItemWrapperTypes());
        }

        private static bool IsSingleAssetType(Type type)
        {
            return typeof(SingleAssetItem<>).IsAssignableFromEx(type);
        }

        private static bool IsMultiAssetType(Type type)
        {
            return typeof(MultiAssetsItem<,>).IsAssignableFromEx(type);
        }

        private static bool IsBaseItemType(Type type)
        {
            return typeof(BaseItem).IsAssignableFromEx(type);
        }

        public static GameObject CreatePrefabAtPath(string path)
        {
            GameObject gameObj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return PrefabUtility.InstantiatePrefab(gameObj) as GameObject;
        }

        
    }

}


