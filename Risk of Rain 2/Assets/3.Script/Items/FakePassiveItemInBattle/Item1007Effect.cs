using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1007Effect : MonoBehaviour
{
    public GameObject _target;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = _target.transform.position;
        if (_target == null)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
