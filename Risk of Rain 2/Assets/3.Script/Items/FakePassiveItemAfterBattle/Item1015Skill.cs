using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//서리유물
public class Item1015Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1015;
    GameObject _item1015 = null;
    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }

        if (GameObject.FindObjectOfType<Item1015SkillComponent>()==null)
        {

            _item1015 = Managers.Resource.Instantiate("Item1015Skill");
            _item1015.GetOrAddComponent<Item1015SkillComponent>();
        }
        else
        {
            _item1015.GetOrAddComponent<Item1015SkillComponent>().ReCall(Managers.ItemInventory.Items[Itemid].Count);

        }
    }

 
}
