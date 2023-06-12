using System.Collections;
using UnityEngine;

public class Gold : MonoBehaviour
{

    GameObject Effect;

    private void OnEnable()
    {
        StartCoroutine(nameof(GoldEffect));
        Effect = Managers.Resource.Instantiate("ItemOutEffect");
        Effect.transform.parent = gameObject.transform;
        float angle = Random.Range(0f, Mathf.PI * 2f);
        GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));


    }
    private IEnumerator GoldEffect()
    {
        yield return new WaitForSeconds(2.0f);
        Managers.Resource.Destroy(gameObject);
    }


}
