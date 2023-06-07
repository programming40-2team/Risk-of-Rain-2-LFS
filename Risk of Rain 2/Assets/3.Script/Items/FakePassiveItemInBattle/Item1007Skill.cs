using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Item1007Skill : NewItemPrimitive, IInBattleItem
{
    public int Itemid => 1007;

    public void InExcuteSkillEffect()
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        if (Util.Probability(100))
        {
            GameObject item1007 = Managers.Resource.Instantiate("Item1007Skill");
            item1007.SetRandomPositionSphere(5, 5, 5, Player.transform);
            GameObject item1007Effect = Managers.Resource.Instantiate("Item1007Effect");
            item1007Effect.GetComponent<Item1007Effect>()._target = item1007;
            item1007.GetOrAddComponent<Item1007SkillComponent>();

        }
  
    }
}
