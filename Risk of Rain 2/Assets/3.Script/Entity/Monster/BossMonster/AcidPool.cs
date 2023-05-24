using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private BeetleQueen _beetleQueen;

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        StartCoroutine(DeleteAcidPool_co());
    }

    IEnumerator DeleteAcidPool_co()
    {
        yield return new WaitForSeconds(15f);
        DeleteAcidPool();
    }

    private void DeleteAcidPool()
    {
        _beetleQueen.AcidPoolPool.ReturnObject(gameObject);
    }
}
