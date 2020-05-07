using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{ 
    [System.Serializable]
    public class FriendItemData : BaseItemData
    {
        [SerializeField]
        private string m_FriendName;

        [SerializeField]
        private int m_HeadID;

        public string FriendName { get => m_FriendName; set => m_FriendName = value; }
        public int HeadID { get => m_HeadID; set => m_HeadID = value; }

        public static FriendItemData Create(string friendName, int headID)
        {
            return new FriendItemData() {
                m_FriendName = friendName,
                m_HeadID = headID,
            };
        }

        
    }
}
