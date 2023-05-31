using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrimitiive : MonoBehaviour
{
    protected GameObject Player;
    protected GameObject Target;
    protected  PlayerStatus _playerStatus;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _playerStatus = Player.GetComponent<PlayerStatus>();
    }
  
}
