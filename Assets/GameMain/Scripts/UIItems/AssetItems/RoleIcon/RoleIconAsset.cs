using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace StarForce
{
    [CreateAssetMenu(fileName = "RoleIconAsset", menuName = "ScriptableObjects/RoleIconAsset", order = 1)]
    public class RoleIconAsset : UIItemAsset<int>
    {

#if UNITY_EDITOR
        public class AddKeyValueResolver : OdinAttributeProcessor<TempKeyValuePair<int, Sprite>>
        {
            public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
            {
                if (member.Name == "Key")
                {
                    //attributes.Add(new EnumToggleButtonsAttribute());
                }
                else if (member.Name == "Value")
                {
                    attributes.Add(new PreviewFieldAttribute(ObjectFieldAlignment.Center));
                }
            }
        }

        public class EditKeyValueResolver : OdinAttributeProcessor<EditableKeyValuePair<int, Sprite>>
        {
            public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
            {
                if (member.Name == "Key")
                {
                    //attributes.Add(new DisplayAsStringAttribute());
                }
                else if (member.Name == "Value")
                {
                    attributes.Add(new PreviewFieldAttribute(ObjectFieldAlignment.Center));
                }
            }
        }
#endif
    }
}
