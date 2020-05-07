using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StarForce
{
    public class FriendItem : BaseItem
    {
        [SerializeField]
        private Text m_FriendName;

        [SerializeField]
        private RoleIconWrapper m_FriendHead;

        public override void SetItem(BaseItemData baseItemData)
        {
            FriendItemData friendItemData = baseItemData as FriendItemData;

            m_FriendName.text = friendItemData.FriendName;

            m_FriendHead.SetRole(friendItemData.HeadID);

        }

        public override void ReshowItem(BaseItemData baseItemData)
        {
            FriendItemData friendItemData = baseItemData as FriendItemData;

            m_FriendName.text = friendItemData.FriendName;

            m_FriendHead.ReshowItem();

        }

    }
}

