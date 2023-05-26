using System.Collections;
using UnityEngine;

public class RangeBomb : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeleteRangeBomb_co());
    }

    private IEnumerator DeleteRangeBomb_co()
    {
        yield return new WaitForSeconds(5f);
        // 폭발
        // 띄우기 / 데미지는 400%
        // 마지막에 Destroy(gameObject);
        // 얘도 오브젝트풀링 할 수는 있는데 해야 하나..? 해야 한다면 바꾸기 / 바꾸는건 쉽잖아
    }
}
