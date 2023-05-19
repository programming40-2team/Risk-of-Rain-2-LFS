using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] GameObject jumpObj;  //점프하는 대상(인스펙터 창에서 넣을 필요없음. 아래 코드로 설정. 이하 점프오브젝트)
    [SerializeField] GameObject goalPoint;  //점프 후 목적지(인스펙터 창에서 넣어야함)

    [Header("점프속도 조절")]
    [SerializeField] [Range(0.001f, 1f)] float jumpSpeed = 0.5f;    //목적지까지 이동하는 속도

    bool isJumping = false;     //점프오브젝트의 점프 상태를 확인하기 위함
    Rigidbody jumpRigidbody;      //점프동안 Rigidbody Gravity를 없애기 위함.


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("접촉");
        jumpObj = other.gameObject;     //접촉하는 게임오브젝트를 점프오브젝트로 설정한다.
        jumpRigidbody = jumpObj.GetComponent<Rigidbody>();
        
        StartCoroutine(JumpGear());     //접촉하면 점프기어 코루틴 동작
        Debug.Log("코루틴 시작");

    }

    

    IEnumerator JumpGear()
    {
        isJumping = true;
        Vector3 goalPos = goalPoint.transform.position;
        
        while (Vector3.SqrMagnitude(jumpObj.transform.position - goalPos)>= 0.05f)
        {
            yield return null;
            jumpRigidbody.useGravity = false;
            jumpObj.transform.position = Vector3.Slerp(jumpObj.transform.position, goalPos, jumpSpeed);
        }
        jumpObj.transform.position = goalPos;
        jumpRigidbody.useGravity = true;
        isJumping = false;

        //    while (isJumping)    //목표지점에 도착할때까지 반복한다
        //    {
        //        yield return null;
        //        jumpRigidbody.useGravity = false;     //부드러운 동작을 위해 점프 동안, 점프 오브젝트의 중력을 없앤다.
        //        Vector3 goalPos = goalPoint.transform.position;
        //        jumpObj.transform.position = Vector3.Slerp(jumpObj.transform.position, goalPos, jumpSpeed);     //SLerp를 이용하여 포물선을 그리며 목적지에 도착하게 된다.
        //        
        //        float distance = Vector3.Distance(jumpObj.transform.position, goalPos);
        //        if (distance < 0.03f)
        //        {
        //            Debug.Log("된다");
        //            jumpRigidbody.useGravity = true;
        //            isJumping = false;
        //        }
        //    }
    }




}
