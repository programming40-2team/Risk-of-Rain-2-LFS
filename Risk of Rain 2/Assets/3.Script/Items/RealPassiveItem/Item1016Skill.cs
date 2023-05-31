using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1016Skill : NewItemPrimitive, IPassiveItem
{
    public int Itemid => 1016;
    private bool isActive = false;

    public void ApplyPassiveEffect()
    {
        if (!Managers.ItemInventory.WhenActivePassiveItem.ContainsKey(Managers.ItemInventory.PassiveItem[Itemid].WhenItemActive))
        {
            return;
        }
        if (!Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always].ContainsKey(Itemid))
        {
            return;
        }

        GameObject Item1016 = null;

        Item1016= Managers.Resource.Instantiate("Item1016Skill");
        Item1016.GetOrAddComponent<Item1016Component>();

    }

}
