using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOutEffect : MonoBehaviour
{
    public Vector3 _targetPosition;
    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, _targetPosition, 0.01f);
        if ((transform.position - _targetPosition).sqrMagnitude < 0.15f)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
