using UnityEditor.Rendering;
using UnityEngine;

//도꺠비불 -> 용암생성
//타겟을 설정해서 타겟일 저장 후 해당 위치에 생성할 수 있음
public class Item1008Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1008;

    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        GameObject item1008 = Managers.Resource.Instantiate("Item1008Skill");
        //콜라이더 있는 경우 위치 조정
        if(TargetTransform.TryGetComponent(out Collider coll))
        {
            item1008.transform.position =new Vector3( TargetTransform.position.x,TargetTransform.position.y -coll.bounds.size.y+3f   ,TargetTransform.position.z);
                
        }
        else
        {
            item1008.transform.position = TargetTransform.position;
        }

        item1008.GetOrAddComponent<Item1008SkillComponent>();
    }
}
