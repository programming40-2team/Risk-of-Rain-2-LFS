using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShip : MonoBehaviour
{
    private Animation _doorOpen;

    void Awake()
    {
        _doorOpen = GetComponent<Animation>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _doorOpen.Play();
        }
    }
}
