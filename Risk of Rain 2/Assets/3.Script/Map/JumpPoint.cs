using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] private GameObject _jumpObj;  //점프하는 대상 (인스펙터 창에서 넣을 필요없음. 아래 코드로 설정. 이하 점프오브젝트)
    [SerializeField] private GameObject _highPoint;  //점프 후 최대 높이 지점 (인스펙터 창에서 넣어야함)
    [SerializeField] private GameObject _goalPoint;  //점프 후 목적지 (인스펙터 창에서 넣어야함)

    [Header("점프속도 조절")]
    [SerializeField][Range(0.001f, 1f)] float jumpSpeed = 0.001f;    //목적지까지 이동하는 속도

    Rigidbody jumpRigidbody;      //점프동안 Rigidbody Gravity를 없애기 위함.


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("접촉");
        _jumpObj = other.gameObject;     //접촉하는 게임오브젝트를 점프오브젝트로 설정한다.
        jumpRigidbody = _jumpObj.GetComponent<Rigidbody>();

        StartCoroutine(JumpGear_co());     //접촉하면 점프기어 코루틴 동작
        Debug.Log("코루틴 시작");
    }



    IEnumerator JumpGear_co()
    {
        Vector3 goalPos = _goalPoint.transform.position;     //도착지점, 인스펙터창에서 추가한 오브젝트의 위치를 받아온다.
        Vector3 highPos = _highPoint.transform.position;     //최대 높이지점

        //while문과 SqrMagnitude 메소드를 통해서 점프하는 오브젝트와 도착지점의 거리를 확인하고, 그 거리가 0.05f보다 멀면 점프 오브젝트의 중력을 false로 만든다

        while (Vector3.SqrMagnitude(_jumpObj.transform.position - highPos) >= 0.05f)
        {
            jumpRigidbody.useGravity = false;
            _jumpObj.transform.position = Vector3.Slerp(_jumpObj.transform.position, highPos, jumpSpeed);

            if (Vector3.SqrMagnitude(_jumpObj.transform.position - highPos) <= 0.05f)
            {
                Debug.Log("중간");
                jumpRigidbody.useGravity = true;

                //while (Vector3.SqrMagnitude(jumpObj.transform.position - goalPos) >= 0.05f)
                //{
                //    Debug.Log("마지막");
                //    //*yield return null;
                //    //*jumpRigidbody.useGravity = false;
                //    //Slerp + transform.position을 통해 포물선으로 이동하며 도착지점까지 이동한다. 점프 오브젝트와 도착지점의 거리가 0.05f 이상이면 코드를 반복해서 이동하게 된다.
                //    jumpObj.transform.position = Vector3.Slerp(jumpObj.transform.position, goalPos, jumpSpeed);
                //}
                //jumpObj.transform.position = goalPos;
                //*jumpRigidbody.useGravity = true;
                yield return null;
            }
        }
    }


}