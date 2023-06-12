using UnityEngine;

public class Item1006Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1006;

    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        Managers.Game.Gold += 5 * Managers.ItemInventory.Items[Itemid].Count;
    }


}
