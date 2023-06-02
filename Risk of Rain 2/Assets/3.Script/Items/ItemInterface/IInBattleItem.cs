using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInBattleItem 
{
    public int Itemid { get; }
    public void InExcuteSkillEffect();

}
