namespace StarForce
{
    public class FriendItemWrapper : BaseItemWrapper<FriendItem, FriendItemData>
    {
        private void OnValidate()
        {
            if (m_Style != ItemStyle.FriendItem)
                m_Style = ItemStyle.FriendItem;
        }
    }
}


