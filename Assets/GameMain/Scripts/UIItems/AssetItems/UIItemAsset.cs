using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIItemAsset<T> : SerializedScriptableObject
{
    [SerializeField]
    private Dictionary<T, Sprite> m_Sprites = new Dictionary<T, Sprite>();

    public virtual Sprite GetSprite(T index)
    {
        m_Sprites.TryGetValue(index, out Sprite sprite);

        return sprite;
    }

}
