using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrimitiive : MonoBehaviour
{
    protected GameObject Player;
    protected GameObject Target;
    protected  PlayerStatus _playerStatus;
    public virtual void Init()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _playerStatus = Player.GetComponent<PlayerStatus>();
    }
    private void Start()
    {
        Init();
    }
  
}
