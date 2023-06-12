using UnityEngine;

public interface IAfterBattleItem
{

    public int Itemid { get; }
    public void AfterExcuteSkillEffect(Transform TargetTransform);

}
