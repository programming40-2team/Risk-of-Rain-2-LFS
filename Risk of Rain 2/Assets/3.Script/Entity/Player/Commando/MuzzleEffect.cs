using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    private WaitForSeconds _effectTime = new WaitForSeconds(0.05f);
    private void OnEnable()
    {
        StartCoroutine(OffEffect_co());
    }

    private IEnumerator OffEffect_co()
    {
        yield return _effectTime;
        gameObject.SetActive(false);
    }
}
