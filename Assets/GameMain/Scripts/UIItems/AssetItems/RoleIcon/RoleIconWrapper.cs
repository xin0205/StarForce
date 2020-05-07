using System.Threading.Tasks;

namespace StarForce
{
    public class RoleIconWrapper : BaseUIItemWrapper<RoleIcon, RoleStyle, int>
    {
        private void Awake()
        {
        }

        private void OnEnable()
        {
            SetRole(m_Type);
        }

        public async Task<RoleIcon> GetUIItem()
        {
            return await GetUIItem((int)m_Style, AssetUtility.GetAssetItemAsset<RoleIcon>());
        }

        public async void SetRole(int roleID)
        {
            m_Type = roleID;

            RoleIcon roleIcon = await GetUIItem();

            roleIcon.SetItem(m_Type);
        }

        void OnValidate()
        {
            if (m_Style == default)
            {
                m_Style = RoleStyle.Head;
            }

            if (m_Type == default)
            {
                m_Type = 1;
            }
        }
    }


}


