using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1011Skill : NewItemPrimitive,IPassiveItem
{
    public int Itemid => 1011;

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.Damage =_playerStatus._survivorsData.Damage+ 20 * Managers.ItemInventory.Items[Itemid].Count;

    }


}
