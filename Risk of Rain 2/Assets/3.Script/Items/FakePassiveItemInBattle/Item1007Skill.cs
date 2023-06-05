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
        if (Util.Probability(10))
        {
            GameObject item1007 = Managers.Resource.Instantiate("Item1007Skill");

            Debug.Log("위치 2개의 직선 을 이어주는 연기 필요");
            Debug.Log("연동방법   함수 (item1007.transform.position ,item1007.SetRandomPositionSphere(2f, 2f, 5, Player.transform);" +
                "기존 SetRandomPosition 지워야함 문의 바람 -KYS ");
            item1007.GetOrAddComponent<Item1007SkillComponent>();
            item1007.SetRandomPositionSphere(2f, 2f, 5, Player.transform);
        }
  
    }
}
