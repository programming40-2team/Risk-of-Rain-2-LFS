using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAfterBattleItem 
{

    public int Itemid { get; }
    public void AfterExcuteSkillEffect();

}
