using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrimitiive : MonoBehaviour
{
    protected GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
  
}
