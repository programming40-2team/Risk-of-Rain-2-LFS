using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassiveItem 
{
    public int Itemid { get; }
    public void ApplyPassiveEffect();

}
