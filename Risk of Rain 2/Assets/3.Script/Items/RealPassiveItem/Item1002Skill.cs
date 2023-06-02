using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1002Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid { get => 1002; }

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.ChanceBlockDamage = 5 * Managers.ItemInventory.Items[Itemid].Count;
    }


}
