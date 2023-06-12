using UnityEngine;

public class Item1013Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1013;

    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }

        GameObject.FindObjectOfType<CommandoSkill>().skillQCoolDownRemain -= 4 + 2 * Managers.ItemInventory.Items[Itemid].Count;
    }


}
