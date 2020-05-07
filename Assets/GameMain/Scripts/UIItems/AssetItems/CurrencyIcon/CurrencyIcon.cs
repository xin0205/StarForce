using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using System;
using System.Collections.Generic;
using System.Reflection;

namespace StarForce
{
    public class CurrencyIcon : MultiAssetsItem<CurrencyStyle, CurrencyType>
    {
#if UNITY_EDITOR
        public class AddKeyValueResolver : OdinAttributeProcessor<TempKeyValuePair<CurrencyStyle, CurrencyIconAsset>>
        {
            public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
            {
                if (member.Name == "Key")
                {
                    attributes.Add(new EnumToggleButtonsAttribute());
                }
                else if (member.Name == "Value")
                {
                    //attributes.Add(new PreviewFieldAttribute(ObjectFieldAlignment.Center));
                }
            }
        }

        public class EditKeyValueResolver : OdinAttributeProcessor<EditableKeyValuePair<CurrencyStyle, CurrencyIconAsset>>
        {
            public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
            {
                if (member.Name == "Key")
                {
                    attributes.Add(new DisplayAsStringAttribute());
                }
                else if (member.Name == "Value")
                {
                    //attributes.Add(new PreviewFieldAttribute(ObjectFieldAlignment.Center));
                }
            }
        }
#endif

    }

}
