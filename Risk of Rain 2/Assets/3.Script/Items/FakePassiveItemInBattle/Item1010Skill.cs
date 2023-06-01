using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1010Skill : NewItemPrimitive,IInBattleItem
{
    public int Itemid => 1010;

    public void InExcuteSkillEffect()
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        _playerStatus.OnHeal(1 * Managers.ItemInventory.Items[Itemid].Count);

    }
}
