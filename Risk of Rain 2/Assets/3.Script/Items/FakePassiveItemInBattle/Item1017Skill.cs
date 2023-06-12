public class Item1017Skill : NewItemPrimitive, IInBattleItem
{
    public int Itemid => 1017;

    public void InExcuteSkillEffect()
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        //적에게 공격이 명중하면 유도갈고리 생성
    }
}
