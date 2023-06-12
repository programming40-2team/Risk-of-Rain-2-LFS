using UnityEngine;

//의식용 단검 3개의 유도 단검 발사
public class Item1014Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1014;

    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        if (Util.Probability(30))
        {
            for (int i = 0; i < 3; i++)
            {
                base.Init();
                GameObject item1014 = Managers.Resource.Instantiate("Item1014Skill");
                item1014.transform.position = Player.transform.position;
                item1014.SetRandomPositionSphere(2f, 2f, 5, Player.transform);
                item1014.GetOrAddComponent<Item1014SkillComponent>();
            }
        }

    }
}
