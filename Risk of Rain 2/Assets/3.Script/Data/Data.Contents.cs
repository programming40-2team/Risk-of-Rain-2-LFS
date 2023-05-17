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
    //해당 형식을 List 로 저장한 후 ? , Dict로 반환 시켜주는 과정입니다.



    #region Item



    [Serializable]
    public class ItemData
    {
        public int itemcode;
        public string name;
        public ItemType itemType;
        public string iconpath;
        public string itemtooltip;
        public int count = 1;

    }
    [Serializable]
    public class WeaponData : ItemData
    {
        public int attack;
        public int magicattack;
        public int range;
        public int available_code;
    }
    [Serializable]
    public class BootData : ItemData
    {
        public int def;
        public int hp;
        public string jobtype;
    }
    [Serializable]
    public class HatData : ItemData
    {
        public int mp;
        public int magicdef;
        public string jobtype;
    }
    [Serializable]
    public class ClothData : ItemData
    {
        public int hp;
        public int def;
        public string jobtype;
    }
    [Serializable]
    public class EarringData : ItemData
    {
        public int attack;
        public int hprecovery;
        public int mprecovery;
    }
    [Serializable]
    public class NecklaceData : ItemData
    {
        public int mp;
        public int attack;
        public int mprecovery;
    }
    [Serializable]
    public class AccessoryData : ItemData
    {
        public int mp;
        public int def;
        public int range;
        public int hprecovery;
    }
    [Serializable]
    public class RingData : ItemData
    {

        public int hprecovery;
        public int mprecovery;

    }
    [Serializable]
    public class ConsumeData : ItemData
    {
        public int value;
    }
    [Serializable]
    public class ETCData : ItemData
    {
    }


    [Serializable]
    public class ItemLoader : ILoader<int, ItemData>
    {
        public List<WeaponData> weapons = new List<WeaponData>();
        public List<BootData> boots = new List<BootData>();
        public List<HatData> hats = new List<HatData>();
        public List<ClothData> cloths = new List<ClothData>();
        public List<EarringData> earings = new List<EarringData>();
        public List<NecklaceData> necklaces = new List<NecklaceData>();
        public List<AccessoryData> accessorys = new List<AccessoryData>();
        public List<RingData> rings = new List<RingData>();
        public List<ConsumeData> consume = new List<ConsumeData>();
        public List<ETCData> etc= new List<ETCData>();

        public Dictionary<int, ItemData> MakeDict()
        {
            Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();
            foreach (ItemData item in weapons)
            {
                item.itemType = ItemType.Weapon;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in boots)
            {
                item.itemType = ItemType.Boot;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in hats)
            {
                item.itemType = ItemType.Hat;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in cloths)
            {
                item.itemType = ItemType.Cloth;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in earings)
            {
                item.itemType = ItemType.Earring;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in necklaces)
            {
                item.itemType = ItemType.Necklace;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in accessorys)
            {
                item.itemType = ItemType.Accessory;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in rings)
            {
                item.itemType = ItemType.Ring;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in consume)
            {
                item.itemType = ItemType.Consume;
                dict.Add(item.itemcode, item);
            }
            foreach (ItemData item in etc)
            {
                item.itemType = ItemType.ETC;
                dict.Add(item.itemcode, item);
            }



            return dict;
        }
    }
    #endregion

    #region Skills
    [Serializable]
    public class Skill
    {
        public int skillcode;
        public string skillName;
        public int skillDamage;
        public string skillInfo;
    }
    [Serializable]
    public class SKillDataLoader : ILoader<int, Skill>
    {
        public List<Skill> Skills = new List<Skill>();
        public Dictionary<int, Skill> MakeDict()
        {
            Dictionary<int, Skill> dict = new Dictionary<int, Skill>();
            foreach (Skill skill in Skills)
            {
                dict.Add(skill.skillcode, skill);
            }
            return dict;
        }
    }



    #endregion
}