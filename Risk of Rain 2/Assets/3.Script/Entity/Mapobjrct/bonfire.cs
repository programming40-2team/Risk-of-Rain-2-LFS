using UnityEngine;

public class bonfire : MonoBehaviour
{
    public float jumpForce = 5f; // 점프 힘

    private Rigidbody playerRigidbody; // 플레이어의 Rigidbody 컴포넌트

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("test"))
        {
            JumpToCube2();
        }
    }

    private void JumpToCube2()
    {
        // cube2로의 점프 힘을 적용
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
