using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewItemPrimitive 
{
    protected GameObject Player;
    protected GameObject Target;
    protected PlayerStatus _playerStatus;

    public virtual void Init()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _playerStatus = Player.GetComponent<PlayerStatus>();
    }
   
}
