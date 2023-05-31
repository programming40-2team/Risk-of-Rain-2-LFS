using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1004Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid { get => 1004; }

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.CriticalChance = _playerStatus._survivorsData.CriticalChance + 10 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[Itemid].WhenItemActive][Itemid].Count;

    }


}
