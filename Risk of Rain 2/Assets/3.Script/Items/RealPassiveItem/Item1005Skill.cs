using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1005Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid { get => 1005; }

    public void ApplyPassiveEffect()
    {
        base.Init();
        _playerStatus.MoveSpeed = _playerStatus._survivorsData.MoveSpeed * 1.11f * Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always][Itemid].Count;


    }


}
