using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1001Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid { get => 1001; }

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.AddMaxHealth(15 * Managers.ItemInventory.Items[Itemid].Count);
    }


}
