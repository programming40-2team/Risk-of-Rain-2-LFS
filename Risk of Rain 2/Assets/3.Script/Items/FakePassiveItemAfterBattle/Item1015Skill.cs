using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//서리유물
public class Item1015Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1015;
    private bool Isitem1015Created;
    GameObject Item1015 = null;
    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        
        if (!Isitem1015Created)
        {
            Item1015 = Managers.Resource.Instantiate("Item1015Skill");
            Item1015.GetOrAddComponent<Item1015SkillComponent>();
            Isitem1015Created = true;
        }
        else
        {

            Item1015.GetOrAddComponent<Item1015SkillComponent>().SetSize(Managers.ItemInventory.Items[Itemid].Count);
        }
    }
}
