using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1022Skill : ItemPrimitiive
{
    [SerializeField] private float movespeed = 50f;
    private bool IsCooltime = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster")||other.CompareTag(Define.BossTag))
        {
            if (!IsCooltime)
            {
                StartCoroutine(nameof(Item1022_co),other.gameObject);
            }
        }
    }
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = Player.transform.forward* movespeed;
        Managers.Resource.Destroy(gameObject, 10.0f);
    }
    private IEnumerator Item1022_co(GameObject go)
    {
        IsCooltime = true;
        go.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(2.0f);
        IsCooltime = false;
    }
    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(nameof(Item1022_co));
    }
}
