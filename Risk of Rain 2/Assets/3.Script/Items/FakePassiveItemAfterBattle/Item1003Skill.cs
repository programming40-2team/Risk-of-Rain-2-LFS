using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Size 키워야 하나?
public class Item1003Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1003;

    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        GameObject item1003 = Managers.Resource.Instantiate("Item1003Skill");
        item1003.transform.position = Player.transform.position;
        item1003.SetRandomPositionSphere(5, 2, 2, TargetTransform);
        item1003.GetOrAddComponent<Item1003SkillComponent>();
    }
}
