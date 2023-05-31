using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemApplyManager 
{
    private Dictionary<int,IPassiveItem> passiveItems;
    private Dictionary<int, IInBattleItem> inBattleItems;
    private Dictionary<int, IAfterBattleItem> afterBattleItems;
    PassiveItemFactory itemFactory;
    public void Init()
    {
        passiveItems = new Dictionary<int,IPassiveItem>();
        inBattleItems = new Dictionary<int, IInBattleItem>();
        afterBattleItems=new Dictionary<int, IAfterBattleItem>();

        itemFactory = new PassiveItemFactory();
        foreach (var key in Managers.ItemInventory.PassiveItem.Keys )
        {
            if (Managers.ItemInventory.PassiveItem[key].WhenItemActive.Equals(Define.WhenItemActivates.Always))
            {
                IPassiveItem passiveItem = itemFactory.CreatePassiveItem(key);
                AddPassiveSkill(passiveItem);
            }
            
        }
        Managers.Event.AddItem += ApplyPassiveSkill;
    }

    public void AddInBattleSkill(IInBattleItem Item)
    {
        inBattleItems.Add(Item.Itemid, Item);
    }
    public void AddAfterBattleSkill(IAfterBattleItem Item)
    {
        afterBattleItems.Add(Item.Itemid, Item);
    }


    public void AddPassiveSkill(IPassiveItem item)
    {
        passiveItems.Add(item.Itemid,item);
    }


    public void ExcuteAfterSkills()
    {
        foreach (var skill in afterBattleItems.Values)
        {
            skill.AfterExcuteSkillEffect();
        }
    }
    public void ExcuteInSkills()
    {
        foreach (var skill in inBattleItems.Values)
        {
            skill.InExcuteSkillEffect();
        }
    }


    public void ApplyPassiveSkills()
    {
        foreach (var skill in passiveItems.Values)
        {
            skill.ApplyPassiveEffect();
        }
    }
    public void ApplyPassiveSkill(int itemcode)
    {
       if( passiveItems.TryGetValue(itemcode, out var skill))
        {
            skill.ApplyPassiveEffect();
        }
    }

}

public class PassiveItemFactory
{
    public IPassiveItem CreatePassiveItem(int itemId)
    {
        switch (itemId)
        {
            case 1001:
                return new Item1001Skill();
            case 1002:
                return new Item1002Skill();
            case 1004:
                return new Item1004Skill();
            case 1005:
                return new Item1005Skill();
            case 1009:
                return new Item1009Skill();
            case 1012:
                return new Item1012Skill();
            case 1016:
                return new Item1016Skill();
            case 1018:
                return new Item1018Skill();
            case 1019:
                return new Item1019Skill();
            default:
                throw new ArgumentException($"Invalid item ID : {itemId}");
        }
    }
}
