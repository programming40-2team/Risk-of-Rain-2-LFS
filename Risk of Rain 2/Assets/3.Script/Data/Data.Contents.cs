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
        public string whenitemactivates;
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
    
    #region Character
    [Serializable]
    public class CharacterData
    {
        public string Name;
        public int charatercode;
        public string script1;
        public string script2;
        public string script3;
        public string script4;
        public string unlockscript1;
        public string unlockscript2;
        public string iconkey;
        public bool isActive;
        public string passiveskill;
        public string m1skill;
        public string m2skill_1;
        public string m2skill_2;
        public string shiftskill_1;
        public string shiftskill_2;
        public string rskill_1;
        public string r_skill2;
        public string passiveskilliconpath;
        public string m1skilliconpath;
        public string m2skill_1iconpath;
        public string m2skill_2iconpath;
        public string shiftskill_1iconpath;
        public string shiftskill_2iconpath;
        public string rskill_1iconpath;
        public string r_skill2iconpath;
        public string skin1;
        public string skin2;
        public string passiveskillscript;
        public string m1skillscript;
        public string m2skill_1script;
        public string m2skill_2script;
        public string shiftskill_1script;
        public string shiftskill_2script;
        public string rskill_1script;
        public string r_skill2script;
        public bool ism2_1skilllearn;
        public bool ism2_2skilllearn;
        public bool isshiftskill_1learn;
        public bool isshiftskill_2learn;
        public bool isrskill_1learn;
        public bool isr_skill2learn;

    }
    [Serializable]
    public class CharacterLoader : ILoader<int, CharacterData>
    {
        public List<CharacterData> characters = new List<CharacterData>();

        public Dictionary<int, CharacterData> MakeDict()
        {
            Dictionary<int, CharacterData> dict = new Dictionary<int, CharacterData>();
            foreach (CharacterData character in characters)
                dict.Add(character.charatercode, character);
            return dict;
        }
    }

    #endregion

    #region Enviroment
    [Serializable]
    public class EnvData
    {
        public int enviromentcode;
        public string enviromentname;
        public string explanation;
        public string imagekey;
        public string monster;

    }
    [Serializable]
    public class EnvLoader : ILoader<int, EnvData>
    {
        public List<EnvData> enviroment = new List<EnvData>();
        public Dictionary<int, EnvData> MakeDict()
        {
            Dictionary<int, EnvData> dict = new Dictionary<int, EnvData>();
            foreach (EnvData item in enviroment)
            {
              
                dict.Add(item.enviromentcode, item);
            }

            return dict;
        }
    }
    #endregion
}