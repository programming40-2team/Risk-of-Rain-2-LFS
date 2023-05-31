using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1018Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid { get => 1018; }

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.AddMaxHealth(40 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[Itemid].WhenItemActive][Itemid].Count);
        _playerStatus.HealthRegen += _playerStatus._survivorsData.HealthRegen + 1.6f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[Itemid].WhenItemActive][Itemid].Count;

    }


}

