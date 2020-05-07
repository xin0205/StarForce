using System.Threading.Tasks;

namespace StarForce
{
    public class CurrencyIconWrapper : BaseUIItemWrapper<CurrencyIcon, CurrencyStyle, CurrencyType>
    {
        private void Awake()
        {
        }

        private void OnEnable()
        {
            SetCurrency(m_Style, m_Type);
        }

        public async Task<CurrencyIcon> GetUIItem()
        {
            return await GetUIItem(Constant.UIItem.CurrencyIconTypeID, AssetUtility.GetAssetItemAsset<CurrencyIcon>());
        }

        public async void SetCurrency(CurrencyStyle currencyStyle, CurrencyType currencyType)
        {
            CurrencyIcon currencyIcon = await GetUIItem();
            currencyIcon.SetItem(currencyStyle, currencyType);
        }

        void OnValidate()
        {
            if (m_Style == default)
            {
                m_Style = CurrencyStyle.Single;
            }
        }
    }


}


