using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0.0f, 100f)] // 스크롤 바
    [SerializeField] // 프라이빗으로 지정하고 유니티에 띄울 수 있도록 해줌
    private float speed = 0f;  //접근 지정자를 생략 시 기본 private
                               //유니티에선 변수를 퍼블릭으로 할 시 유니티 상에 수정 가능하게 표기됨
                               // 코드상에서 변수명을 바꿀 시 유니티에서 입력한 값이 초기화됨
                               // c#은 m_ 전역벽수가 없으므로 멤버변수 구분이 필요 없음

 
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            MoveToDir(Vector3.forward);

        if (Input.GetKey(KeyCode.S))
            MoveToDir(Vector3.back);
        if (Input.GetKey(KeyCode.A))
            MoveToDir(Vector3.left);
        if (Input.GetKey(KeyCode.D))
            MoveToDir(Vector3.right);
    }


    public void MoveToDir(Vector3 _dir)
    {
        //Transform이란 유니티가 만들어논 클래스를 가져와서 사용
        //Vector3 pos = this.GetComponent<Transform>().position; // 밑에 껄 풀어 씀
        Vector3 newPos = this.transform.position;
        newPos = newPos + (_dir * speed * Time.deltaTime); //업뎃에서 가저올 필요 없이 Time에서 가져옴
        transform.position = newPos;
    }
}
