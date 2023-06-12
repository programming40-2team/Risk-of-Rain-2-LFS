using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemApplyManager
{
    private Dictionary<int, IPassiveItem> passiveItems;
    private Dictionary<int, IInBattleItem> inBattleItems;
    private Dictionary<int, IAfterBattleItem> afterBattleItems;
    PassiveItemFactory itemFactory;
    public void Init()
    {
        passiveItems = new Dictionary<int, IPassiveItem>();
        inBattleItems = new Dictionary<int, IInBattleItem>();
        afterBattleItems = new Dictionary<int, IAfterBattleItem>();

        itemFactory = new PassiveItemFactory();
        //foreach (var key in Managers.ItemInventory.PassiveItem.Keys )
        //{
        //    if (Managers.ItemInventory.PassiveItem[key].WhenItemActive.Equals(Define.WhenItemActivates.Always))
        //    {

        //        AddPassiveSkill(passiveItem);
        //    }
        //    else if (Managers.ItemInventory.PassiveItem[key].WhenItemActive.Equals(Define.WhenItemActivates.InBattle)) 
        //    {
        //        IInBattleItem inbattleitem = itemFactory.CreateInBattleItem(key);
        //        AddInBattleSkill(inbattleitem);
        //    }
        //    else if (Managers.ItemInventory.PassiveItem[key].WhenItemActive.Equals(Define.WhenItemActivates.AfterBattle))
        //    {
        //        IAfterBattleItem afterBattleimte = itemFactory.CreateAfterBattleItem(key);
        //        AddAfterBattleSkill(afterBattleimte);
        //    }


        //}
    }

    public void AddInBattleSkill(int itemcode)
    {
        if (inBattleItems.ContainsKey(itemcode))
        {
            return;
        }
        IInBattleItem inbattleitem = itemFactory.CreateInBattleItem(itemcode);
        inBattleItems.Add(inbattleitem.Itemid, inbattleitem);
    }
    //public void AddAfterBattleSkill(IAfterBattleItem Item)
    //{
    //    afterBattleItems.Add(Item.Itemid, Item);
    //}
    public void AddAfterBattleSkill(int itemcode)
    {
        if (afterBattleItems.ContainsKey(itemcode))
        {
            return;
        }
        IAfterBattleItem afterBattleimte = itemFactory.CreateAfterBattleItem(itemcode);
        afterBattleItems.Add(afterBattleimte.Itemid, afterBattleimte);
    }

    public void AddPassiveSkill(int itemcode)
    {
        if (passiveItems.ContainsKey(itemcode))
        {
            return;
        }
        IPassiveItem passiveItem = itemFactory.CreatePassiveItem(itemcode);
        passiveItems.Add(passiveItem.Itemid, passiveItem);
    }



    //적이 죽으면 적 위치를 넣어서 모두 사용
    public void ExcuteAfterSkills(Transform TargetTranform)
    {
        foreach (var skill in afterBattleItems.Values)
        {
            skill.AfterExcuteSkillEffect(TargetTranform);
        }
    }
    //적이 피해를 입으면 모두 사용
    public void ExcuteInSkills()
    {
        foreach (var skill in inBattleItems.Values)
        {
            skill.InExcuteSkillEffect();
        }
    }

    //씬 갱신 시에만 사용할 -> Apply Passive Skills 
    public void ApplyPassiveSkills()
    {
        foreach (var skill in passiveItems.Values)
        {
            skill.ApplyPassiveEffect();
        }
    }
    public void ApplyPassiveSkill(int itemcode)
    {
        if (passiveItems.TryGetValue(itemcode, out var skill))
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
            case 1011:
                return new Item1011Skill();
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
    public IInBattleItem CreateInBattleItem(int itemId)
    {

        switch (itemId)
        {
            case 1007:
                return new Item1007Skill();
            case 1010:
                return new Item1010Skill();
            case 1017:
                return new Item1017Skill();
            default:
                throw new ArgumentException($"Invalid item ID : {itemId}");
        }
    }
    public IAfterBattleItem CreateAfterBattleItem(int itemId)
    {
        switch (itemId)
        {
            case 1003:
                return new Item1003Skill();
            case 1006:
                return new Item1006Skill();
            case 1008:
                return new Item1008Skill();
            case 1013:
                return new Item1013Skill();
            case 1014:
                return new Item1014Skill();
            case 1015:
                return new Item1015Skill();
            default:
                throw new ArgumentException($"Invalid item ID : {itemId}");
        }
    }
}
