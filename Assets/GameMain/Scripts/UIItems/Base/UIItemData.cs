using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemData : EntityData
{
    private Transform m_Parent;

    public Transform Parent { get => m_Parent; set => m_Parent = value; }

    public UIItemData(int entityId, int typeID, Transform parent)
            : base(entityId, typeID)
    {
        Parent = parent;
    }

    public static UIItemData Create(int entityId, int typeID, Transform parent)
    {
        return new UIItemData(entityId, typeID, parent);
    }


}
