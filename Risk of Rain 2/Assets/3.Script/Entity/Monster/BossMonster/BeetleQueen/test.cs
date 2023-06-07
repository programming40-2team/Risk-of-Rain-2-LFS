using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(transform.root.name);
    }
}
