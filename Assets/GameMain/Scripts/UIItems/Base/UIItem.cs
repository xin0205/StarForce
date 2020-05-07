using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class UIItem : Entity
    {
        protected UIItemData m_UIItemData = null;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            OriginalTransform = CachedTransform.parent;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_UIItemData = userData as UIItemData;
            if (m_UIItemData == null)
            {
                Log.Error("Bullet data is invalid.");
                return;
            }

            if (m_UIItemData.Parent != null)
            {
                CachedTransform.SetParent(m_UIItemData.Parent);
                CachedTransform.localPosition = m_UIItemData.Position;
            }

        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            CachedTransform.SetParent(OriginalTransform);
        }
    }
}
