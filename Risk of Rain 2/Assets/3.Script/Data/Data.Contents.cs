using System;
using System.Collections.Generic;
using static Define;

namespace Data
{
    //각종 데이터들
    //데이터는 JSON 형식으로 구현합니다.
    //ILoader Interface를 상속받은 class 는 Dictionary를 반환하는
    //MakeDict() 함수를 반드시 구현해야합니다.
    //Data의 형식과 데이터를 받아와
    //해당 형식을 List 로 저장한 후 , Dict로 반환 시켜주는 과정입니다.
    #region Item
    [Serializable]
    public class ItemData
    {
        public int itemcode;
        public string itemname;
        public string explanation;
        public ItemType itemType;
        public string iconkey;
        public bool isHaveHad;
        public int count;

    }
    [Serializable]
    public class PassiveData : ItemData
    {
        public string itemwhenactivates;
        public int tier;
    }
    [Serializable]
    public class ActivaData : ItemData
    {
        public int cooltime;
    }

    [Serializable]
    public class ItemLoader : ILoader<int, ItemData>
    {
        public List<PassiveData> Passive = new List<PassiveData>();
        public List<ActivaData> Active = new List<ActivaData>();
        public Dictionary<int, ItemData> MakeDict()
        {
            Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();
            foreach (ItemData item in Passive)
            {
                item.itemType = ItemType.Passive;
                dict.Add(item.itemcode, item);
            }
            foreach(ItemData item in Active)
            {
                item.itemType = ItemType.Active;
                dict.Add(item.itemcode, item);
            }
            return dict;
        }
    }
    #endregion

}