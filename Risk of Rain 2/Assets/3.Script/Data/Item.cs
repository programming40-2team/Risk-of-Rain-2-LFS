using Data;
using System;
using static Define;

public class Item
{

    //아이템 입니다. 아이템 데이터에서 데이터를 받아와
    //실제 사용될 아이템으로 넣어줍니다.
    //maKEiTEM의 경우 던전 클리어 보상, 보물상자 보상 ,뽑기 보상 등
    //다양하게 아이템을 유저에게 건넬 떄 사용하기에 sTATIC으로 구현했습니다.
    // 공통적인 속성은 ITem으로, 나머지 item의 경우 타입에 따라 다르게 받을 수 있도록 설정했습니다.
    public ItemData Info { get; } = new ItemData();

    public int ItemCode { get { return Info.itemcode; } set { Info.itemcode = value; } }
    public string ItemName { get { return Info.itemname; } set { Info.itemname = value; } }
    public string ItemExplanation { get { return Info.explanation; } set { Info.explanation = value; } }
    public string ItemIconKey { get { return Info.iconkey; } set { Info.iconkey = value; } }
    public int Count { get { return Info.count; } set { Info.count = value; } }
    public ItemType ItemType { get; private set; }


    public Item(ItemType itemType)
    {
        ItemType = itemType;
    }
    public static Item MakeItem(ItemData itemInfo)
    {
        Item item = null;
        ItemData itemData = null;
        Managers.Data.ItemDataDict.TryGetValue(itemInfo.itemcode, out itemData);
        if (itemData == null)
            return null;
        switch (itemData.itemType)
        {
            case ItemType.Passive:
                item = new PassiveItem(itemInfo.itemcode);
                break;
            case ItemType.Active:
                item = new ActiveItem(itemInfo.itemcode);
                break;
        }
        if (item != null)
        {
            item.ItemCode = itemInfo.itemcode;
            item.ItemName = itemInfo.itemname;
            item.ItemIconKey = itemInfo.iconkey;
            item.ItemExplanation = itemInfo.explanation;
            item.Count = itemInfo.count;
        }
        return item;
    }
    public class ActiveItem : Item
    {
        public int Cooltime { get; private set; }
        public ActiveItem(int templateId) : base(ItemType.Active)
        {
            Init(templateId);
        }
        void Init(int templateId)
        {
            ItemData itemData = null;
            Managers.Data.ItemDataDict.TryGetValue(templateId, out itemData);
            if (itemData.itemType != ItemType.Active)
                return;
            ActivaData data = (ActivaData)itemData;
            {
                Cooltime = data.cooltime;
            }
        }
    }
    public class PassiveItem : Item
    {
        public int Tier { get; private set; }
        public WhenItemActivates WhenItemActive { get; private set; }
        public PassiveItem(int templateId) : base(ItemType.Passive)
        {
            Init(templateId);
        }
        void Init(int templateId)
        {
            ItemData itemData = null;
            Managers.Data.ItemDataDict.TryGetValue(templateId, out itemData);
            if (itemData.itemType != ItemType.Passive)
                return;
            PassiveData data = (PassiveData)itemData;
            {
                Tier = data.tier;
                WhenItemActive = (WhenItemActivates)Enum.Parse(typeof(WhenItemActivates), data.whenitemactivates);
            }
        }
    }

}